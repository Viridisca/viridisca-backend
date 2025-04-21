using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Viridisca.Common.Application.Exceptions;
using Viridisca.Common.Domain;
using Viridisca.Modules.Identity.Domain.Models;
using Viridisca.Modules.Identity.Domain.Repositories;
using Viridisca.Modules.Identity.Domain.Services;
using Viridisca.Common.Application.Data;

namespace Viridisca.Modules.Identity.Application.Authentication.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUnitOfWork _unitOfWork;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TokenResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Validate email or username
        var user = await _userRepository.GetByEmailAsync(request.EmailOrUsername, cancellationToken);
        
        if (user == null)
        {
            // Try by username if not found by email
            user = await _userRepository.GetByUsernameAsync(request.EmailOrUsername, cancellationToken);
            
            if (user == null)
            {
                return Result.Failure<TokenResult>(
                    Error.Failure("Authentication.InvalidCredentials", "Invalid email or password"));
            }
        }
        
        // Validate account status
        if (!user.IsActive)
        {
            return Result.Failure<TokenResult>(
                Error.Failure("Authentication.AccountDisabled", "Account is disabled"));
        }
        
        // Validate password
        if (!_passwordHasher.VerifyPassword(passwordHash: user.PasswordHash, providedPassword: request.Password))
        {
            return Result.Failure<TokenResult>(
                Error.Failure("Authentication.InvalidCredentials", "Invalid email or password"));
        }
        
        // Record login
        var loginResult = user.RecordLogin();
        if (loginResult.IsFailure)
        {
            return Result.Failure<TokenResult>(loginResult.Error);
        }
        
        // Generate tokens
        var tokenResult = await _jwtProvider.GenerateTokenAsync(user, cancellationToken);
        if (tokenResult.IsFailure)
        {
            return Result.Failure<TokenResult>(tokenResult.Error);
        }
        
        var tokenResponse = tokenResult.Value;
        
        // Create refresh token entity
        var refreshTokenResult = Viridisca.Modules.Identity.Domain.Models.RefreshToken.Create(
            tokenResponse.RefreshToken,
            user.Uid,
            DateTime.UtcNow.AddDays(7));

        if (refreshTokenResult.IsFailure)
        {
            return Result.Failure<TokenResult>(refreshTokenResult.Error);
        }
        
        // Save refresh token
        var addTokenResult = user.AddRefreshToken(refreshTokenResult.Value);
        if (addTokenResult.IsFailure)
        {
            return Result.Failure<TokenResult>(addTokenResult.Error);
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = new TokenResult(
            tokenResponse.AccessToken,
            tokenResponse.RefreshToken,
            tokenResponse.ExpiresIn,
            user.Username ?? user.Email,
            user.Email);
            
        return Result.Success(result);
    }
} 