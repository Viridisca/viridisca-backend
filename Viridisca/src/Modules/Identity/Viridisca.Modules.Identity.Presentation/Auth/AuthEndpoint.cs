using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Viridisca.Common.Application.Data;
using Viridisca.Common.Presentation;
using Viridisca.Common.Presentation.Endpoints;
using Viridisca.Modules.Identity.Domain.Models;
using Viridisca.Modules.Identity.Domain.Repositories;
using Viridisca.Modules.Identity.Domain.Services;

namespace Viridisca.Modules.Identity.Presentation.Auth;

/// <summary>
/// Authentication endpoints
/// </summary>
internal sealed class AuthEndpoint : IEndpoint
{
    /// <summary>
    /// Maps the authentication endpoints
    /// </summary>
    /// <param name="builder">The endpoint route builder</param>
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        var authGroup = builder.MapGroup("/api/identity").WithTags(Tags.Authentication);

        authGroup.MapPost("/login", Login);
        authGroup.MapPost("/register", Register);
        authGroup.MapPost("/refresh-token", RefreshTokenEndpoint);
        authGroup.MapPost("/logout", Logout).RequireAuthorization();
    }

    /// <summary>
    /// Login endpoint
    /// </summary>
    private static async Task<IResult> Login(
        LoginRequest request,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        // Find user by email
        var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);
        
        // Check if user exists and password is correct
        if (user == null || !passwordHasher.VerifyPassword(passwordHash: user.PasswordHash, providedPassword: request.Password))
        {
            return Results.Problem(
                title: "Authentication Failed",
                detail: "Invalid email or password",
                statusCode: StatusCodes.Status401Unauthorized);
        }

        // Validate account status
        if (!user.IsActive)
        {
            return Results.Problem(
                title: "Authentication Failed",
                detail: "Account is disabled",
                statusCode: StatusCodes.Status401Unauthorized);
        }

        // Record login
        var loginResult = user.RecordLogin();
        if (loginResult.IsFailure)
        {
            return Results.Problem(
                title: "Authentication Error",
                detail: loginResult.Error.Description,
                statusCode: StatusCodes.Status500InternalServerError);
        }
        
        // Generate tokens
        var accessToken = jwtProvider.GenerateAccessToken(user);
        var refreshToken = jwtProvider.GenerateRefreshToken();
        
        // Create refresh token entity
        var refreshTokenResult = RefreshToken.Create(
            refreshToken,
            user.Uid,
            DateTime.UtcNow.AddDays(7));
        
        if (refreshTokenResult.IsFailure)
        {
            return Results.Problem(
                title: "Token Generation Error",
                detail: refreshTokenResult.Error.Description,
                statusCode: StatusCodes.Status500InternalServerError);
        }
        
        // Save refresh token
        var addTokenResult = user.AddRefreshToken(refreshTokenResult.Value);
        if (addTokenResult.IsFailure)
        {
            return Results.Problem(
                title: "Token Storage Error",
                detail: addTokenResult.Error.Description,
                statusCode: StatusCodes.Status500InternalServerError);
        }
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        // Return tokens
        return Results.Ok(new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }

    /// <summary>
    /// Register endpoint
    /// </summary>
    private static async Task<IResult> Register(
        RegisterRequest request,
        IPasswordHasher passwordHasher,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        // Check if email already exists
        if (await userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
        {
            return Results.Problem(
                title: "Registration Failed",
                detail: "Email already exists",
                statusCode: StatusCodes.Status400BadRequest);
        }

        // Hash password
        string passwordHash = passwordHasher.HashPassword(request.Password);
        
        // Create user
        var userResult = User.Create(
            Guid.NewGuid(), 
            request.Email, 
            request.FirstName, 
            request.LastName, 
            passwordHash);
        
        if (userResult.IsFailure)
        {
            return Results.Problem(
                title: "Registration Failed",
                detail: userResult.Error.Description,
                statusCode: StatusCodes.Status400BadRequest);
        }
        
        // Add user
        userRepository.Insert(userResult.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Results.Ok();
    }

    /// <summary>
    /// Refresh token endpoint
    /// </summary>
    private static async Task<IResult> RefreshTokenEndpoint(
        RefreshTokenRequest request,
        IJwtProvider jwtProvider,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        // Find refresh token
        var refreshToken = await userRepository.GetRefreshTokenAsync(request.RefreshToken, cancellationToken);
        
        // Check if token exists and is valid
        if (refreshToken == null || refreshToken.ExpiryDate < DateTime.UtcNow)
        {
            return Results.Problem(
                title: "Token Refresh Failed",
                detail: "Invalid or expired refresh token",
                statusCode: StatusCodes.Status401Unauthorized);
        }

        // Generate new tokens
        var accessToken = jwtProvider.GenerateAccessToken(refreshToken.User);
        var newRefreshToken = jwtProvider.GenerateRefreshToken();
        
        // Revoke old refresh token
        var revokeResult = refreshToken.Revoke(
            reason: "Replaced with a new token",
            replacedByToken: newRefreshToken);
            
        if (revokeResult.IsFailure)
        {
            return Results.Problem(
                title: "Token Refresh Failed",
                detail: revokeResult.Error.Description,
                statusCode: StatusCodes.Status500InternalServerError);
        }
        
        // Remove old refresh token
        userRepository.RemoveRefreshToken(refreshToken);
        
        // Create new refresh token entity
        var newRefreshTokenResult = RefreshToken.Create(
            newRefreshToken,
            refreshToken.UserUid,
            DateTime.UtcNow.AddDays(7));
            
        if (newRefreshTokenResult.IsFailure)
        {
            return Results.Problem(
                title: "Token Creation Failed",
                detail: newRefreshTokenResult.Error.Description,
                statusCode: StatusCodes.Status500InternalServerError);
        }
        
        // Save new refresh token
        userRepository.AddRefreshToken(newRefreshTokenResult.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        // Return tokens
        return Results.Ok(new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken
        });
    }
    
    /// <summary>
    /// Logout endpoint
    /// </summary>
    private static async Task<IResult> Logout(
        LogoutRequest request,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        // Find refresh token
        var refreshToken = await userRepository.GetRefreshTokenAsync(request.RefreshToken, cancellationToken);
        
        // If token exists, revoke and remove it
        if (refreshToken != null)
        {
            var revokeResult = refreshToken.Revoke(reason: "User logout");
            if (revokeResult.IsFailure)
            {
                return Results.Problem(
                    title: "Logout Failed",
                    detail: revokeResult.Error.Description,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            
            userRepository.RemoveRefreshToken(refreshToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        
        return Results.Ok();
    }
}

/// <summary>
/// Login request
/// </summary>
public sealed class LoginRequest
{
    /// <summary>
    /// Gets or sets the email
    /// </summary>
    public required string Email { get; init; }
    
    /// <summary>
    /// Gets or sets the password
    /// </summary>
    public required string Password { get; init; }
}

/// <summary>
/// Register request
/// </summary>
public sealed class RegisterRequest
{
    /// <summary>
    /// Gets or sets the email
    /// </summary>
    public required string Email { get; init; }
    
    /// <summary>
    /// Gets or sets the password
    /// </summary>
    public required string Password { get; init; }
    
    /// <summary>
    /// Gets or sets the first name
    /// </summary>
    public required string FirstName { get; init; }
    
    /// <summary>
    /// Gets or sets the last name
    /// </summary>
    public required string LastName { get; init; }
}

/// <summary>
/// Authentication response
/// </summary>
public sealed class AuthResponse
{
    /// <summary>
    /// Gets or sets the access token
    /// </summary>
    public required string AccessToken { get; init; }
    
    /// <summary>
    /// Gets or sets the refresh token
    /// </summary>
    public required string RefreshToken { get; init; }
}

/// <summary>
/// Refresh token request
/// </summary>
public sealed class RefreshTokenRequest
{
    /// <summary>
    /// Gets or sets the refresh token
    /// </summary>
    public required string RefreshToken { get; init; }
}

/// <summary>
/// Logout request
/// </summary>
public sealed class LogoutRequest
{
    /// <summary>
    /// Gets or sets the refresh token
    /// </summary>
    public required string RefreshToken { get; init; }
} 