using Viridisca.Common.Domain;

namespace Viridisca.Modules.Identity.Domain.Models;

/// <summary>
/// Represents a user in the system
/// </summary>
public class User : Entity
{
    private readonly List<UserRole> _userRoles = [];
    private readonly List<RefreshToken> _refreshTokens = [];

    /// <summary>
    /// Gets or sets the unique identifier of the user
    /// </summary>
    public Guid Uid { get; private set; }

    /// <summary>
    /// Gets or sets the email of the user
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Gets or sets the first name of the user
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Gets or sets the last name of the user
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Gets or sets the hashed password of the user
    /// </summary>
    public string PasswordHash { get; private set; }

    /// <summary>
    /// Gets or sets the creation date of the user
    /// </summary>
    public DateTime CreatedDate { get; private set; }

    /// <summary>
    /// Gets or sets the username of the user
    /// </summary>
    public string Username { get; private set; }

    /// <summary>
    /// Gets or sets the middle name of the user
    /// </summary>
    public string MiddleName { get; private set; }

    /// <summary>
    /// Gets or sets the phone number of the user
    /// </summary>
    public string PhoneNumber { get; private set; }

    /// <summary>
    /// Gets or sets the profile image URL of the user
    /// </summary>
    public string ProfileImageUrl { get; private set; }

    /// <summary>
    /// Gets or sets the date of birth of the user
    /// </summary>
    public DateTime DateOfBirth { get; private set; }

    /// <summary>
    /// Gets or sets the security stamp of the user
    /// </summary>
    public string SecurityStamp { get; private set; }

    /// <summary>
    /// Gets or sets whether the email is confirmed
    /// </summary>
    public bool IsEmailConfirmed { get; private set; }

    /// <summary>
    /// Gets or sets whether the user is active
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    /// Gets or sets the creation date of the user in UTC
    /// </summary>
    public DateTime CreatedAtUtc { get; private set; }

    /// <summary>
    /// Gets or sets the last login date of the user in UTC
    /// </summary>
    public DateTime? LastLoginAtUtc { get; private set; }

    /// <summary>
    /// Gets or sets the last modified date of the user in UTC
    /// </summary>
    public DateTime? LastModifiedAtUtc { get; private set; }

    /// <summary>
    /// Gets the roles of the user
    /// </summary>
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    /// <summary>
    /// Gets the refresh tokens of the user
    /// </summary>
    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    /// <summary>
    /// Default constructor for EF Core
    /// </summary>
    protected User()
    {
    }

    /// <summary>
    /// Creates a new user
    /// </summary>
    private User(Guid uid, string email, string firstName, string lastName, string passwordHash)
    {
        Uid = uid;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        PasswordHash = passwordHash;
        CreatedDate = DateTime.UtcNow;
        CreatedAtUtc = DateTime.UtcNow;
        IsActive = true;
        Username = email;
        SecurityStamp = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Factory method to create a new user
    /// </summary>
    public static Result<User> Create(Guid uid, string email, string firstName, string lastName, string passwordHash)
    {
        if (uid == Guid.Empty)
        {
            return Result.Failure<User>(Error.Failure("User.InvalidId", "User ID cannot be empty"));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            return Result.Failure<User>(Error.Failure("User.InvalidEmail", "Email cannot be empty"));
        }

        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Result.Failure<User>(Error.Failure("User.InvalidFirstName", "First name cannot be empty"));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            return Result.Failure<User>(Error.Failure("User.InvalidLastName", "Last name cannot be empty"));
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            return Result.Failure<User>(Error.Failure("User.InvalidPasswordHash", "Password hash cannot be empty"));
        }

        return Result.Success(new User(uid, email, firstName, lastName, passwordHash));
    }

    /// <summary>
    /// Updates the user information
    /// </summary>
    public Result Update(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Result.Failure(Error.Failure("User.InvalidFirstName", "First name cannot be empty"));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            return Result.Failure(Error.Failure("User.InvalidLastName", "Last name cannot be empty"));
        }

        FirstName = firstName;
        LastName = lastName;
        LastModifiedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }

    /// <summary>
    /// Updates the user information
    /// </summary>
    public Result UpdatePersonalInfo(string firstName, string lastName, string middleName, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Result.Failure(Error.Failure("User.InvalidFirstName", "First name cannot be empty"));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            return Result.Failure(Error.Failure("User.InvalidLastName", "Last name cannot be empty"));
        }

        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        PhoneNumber = phoneNumber;
        LastModifiedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }

    /// <summary>
    /// Updates the email of the user
    /// </summary>
    public Result UpdateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result.Failure(Error.Failure("User.InvalidEmail", "Email cannot be empty"));
        }

        if (Email != email.Normalize().Trim().ToLowerInvariant())
        {
            Email = email.Normalize().Trim().ToLowerInvariant();
            IsEmailConfirmed = false;
            SecurityStamp = Guid.NewGuid().ToString();
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        return Result.Success();
    }

    /// <summary>
    /// Confirms the email of the user
    /// </summary>
    public Result ConfirmEmail()
    {
        if (IsEmailConfirmed)
        {
            return Result.Failure(Error.Failure("User.EmailAlreadyConfirmed", "Email is already confirmed"));
        }

        IsEmailConfirmed = true;
        LastModifiedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }

    /// <summary>
    /// Updates the password of the user
    /// </summary>
    public Result UpdatePassword(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            return Result.Failure(Error.Failure("User.InvalidPasswordHash", "Password hash cannot be empty"));
        }

        PasswordHash = passwordHash;
        SecurityStamp = Guid.NewGuid().ToString();
        LastModifiedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }

    public Result UpdateProfileImage(string profileImageUrl)
    {
        ProfileImageUrl = profileImageUrl;
        LastModifiedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }

    public Result Activate()
    {
        if (IsActive)
        {
            return Result.Failure(Error.Failure("User.AlreadyActive", "User is already active"));
        }

        IsActive = true;
        LastModifiedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }

    public Result Deactivate()
    {
        if (!IsActive)
        {
            return Result.Failure(Error.Failure("User.AlreadyInactive", "User is already inactive"));
        }

        IsActive = false;
        LastModifiedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }

    public Result RecordLogin()
    {
        LastLoginAtUtc = DateTime.UtcNow;
        return Result.Success();
    }

    public Result AddRole(UserRole role)
    {
        if (role == null)
        {
            return Result.Failure(Error.Failure("User.InvalidRole", "Role cannot be null"));
        }

        _userRoles.Add(role);
        LastModifiedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }

    public Result RemoveRole(UserRole role)
    {
        if (role == null)
        {
            return Result.Failure(Error.Failure("User.InvalidRole", "Role cannot be null"));
        }

        if (!_userRoles.Contains(role))
        {
            return Result.Failure(Error.Failure("User.RoleNotFound", "User does not have this role"));
        }

        _userRoles.Remove(role);
        LastModifiedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }

    public Result AddRefreshToken(RefreshToken token)
    {
        if (token == null)
        {
            return Result.Failure(Error.Failure("User.InvalidToken", "Token cannot be null"));
        }

        _refreshTokens.Add(token);
        LastModifiedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }

    public Result RemoveRefreshToken(RefreshToken token)
    {
        if (token == null)
        {
            return Result.Failure(Error.Failure("User.InvalidToken", "Token cannot be null"));
        }

        if (!_refreshTokens.Contains(token))
        {
            return Result.Failure(Error.Failure("User.TokenNotFound", "User does not have this token"));
        }

        _refreshTokens.Remove(token);
        LastModifiedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }
}
