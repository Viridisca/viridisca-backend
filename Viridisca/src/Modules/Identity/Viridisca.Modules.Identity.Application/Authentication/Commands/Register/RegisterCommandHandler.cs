using MediatR;
using Viridisca.Common.Application.Exceptions;
using Viridisca.Common.Domain;
using Viridisca.Modules.Identity.Domain.Models;
using Viridisca.Modules.Identity.Domain.Services;
using Viridisca.Common.Application.Data;

namespace Viridisca.Modules.Identity.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResult>
{
    private readonly Domain.Repositories.IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(
        Domain.Repositories.IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Check if email already exists
        if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
        {
            throw new ViridiscaException(
                "Registration", 
                new Error("Registration.EmailExists", "Email already registered", ErrorType.Validation));
        }
        
        // Hash password
        string passwordHash = _passwordHasher.HashPassword(request.Password);
        
        // Create user
        var user = User.Create(
           Guid.Empty,
           request.Email,
           // request.Username,
           request.FirstName,
           request.LastName,
           // request.PhoneNumber,
           passwordHash);
        // request.DateOfBirth);

        // Set initial properties
        user.Activate();
        
        // Add user to database
        _userRepository.Insert(user.Value);
        
        // Save changes
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        // Return result
        return new RegisterResult(user.Value.Uid, user.Value.Email);
    }
} 