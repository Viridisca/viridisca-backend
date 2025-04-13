using System.ComponentModel;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Lessons.Domain.Lessons;

/// <summary>
/// Урок
/// </summary>
public class Lesson : Entity
{
    public Guid Uid { get; set; }

    public DateTime StartsAtUtc { get; set; }
    public DateTime EndsAtUtc { get; set; }

    public string Subject { get; set; } // todo: podv. перенести предмет в сущность
    public string Classroom { get; set; } // todo: podv. перенести кабинет в сущность?

    public string Description { get; set; }

    // public Group Group { get; set; }
    public Guid GroupUid { get; set; }

    // public Teacher Teacher { get; set; }
    public Guid TeacherUid { get; set; }

    //public ICollection<Attendance> Attendances { get; set; }
    //public ICollection<Grade> Grades { get; set; }

    //public ICollection<RetakeRequest> RetakeRequests { get; set; }

    public static Result<Lesson> Create(
        string subject,
        string description,
        string classroom,
        DateTime startsAtUtc,
        DateTime endsAtUtc,
        Guid groupUid,
        Guid teacherUid)
    {
        if (endsAtUtc < startsAtUtc)
        {
            return Result.Failure<Lesson>(LessonErrors.EndDatePrecedesStartDate);
        }

        var lesson = new Lesson
        {
            Uid = Guid.NewGuid(),
            Subject = subject,
            Description = description,
            Classroom = classroom,
            StartsAtUtc = startsAtUtc,
            EndsAtUtc = endsAtUtc,
            GroupUid = groupUid,
            TeacherUid = teacherUid
        };

        lesson.Raise(new LessonCreatedDomainEvent(lesson.Uid));
        return lesson;
    }
}


// ------------------------- Other ------------------------- //

//// Domain/Entities/Identity/User.cs
//public class User : Entity
//{
//    public Guid Uid { get; private set; }
//    public string Email { get; private set; }
//    public string PasswordHash { get; private set; }
//    public string SecurityStamp { get; private set; }
//    public bool IsEmailConfirmed { get; private set; }
//    public DateTime CreatedAtUtc { get; private set; }
//    public DateTime? LastLoginAtUtc { get; private set; }

//    // Relationships
//    private readonly List<UserRole> _userRoles = new();
//    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

//    private readonly List<RefreshToken> _refreshTokens = new();
//    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

//    // Методы для бизнес-логики
//    public static Result<User> Create(string email, IPasswordHasher passwordHasher)
//    {
//        // Validation logic
//        var user = new User
//        {
//            Uid = Guid.NewGuid(),
//            Email = email.Normalize().Trim().ToLowerInvariant(),
//            CreatedAtUtc = DateTime.UtcNow,
//            SecurityStamp = Guid.NewGuid().ToString()
//        };

//        var passwordResult = user.SetPassword(passwordHasher, "TempPassword");
//        if (passwordResult.IsFailure)
//            return Result.Failure<User>(passwordResult.Error);

//        user.Raise(new UserCreatedDomainEvent(user.Uid));
//        return user;
//    }

//    public Result SetPassword(IPasswordHasher passwordHasher, string newPassword)
//    {
//        // Password policy validation
//        var hashResult = passwordHasher.HashPassword(newPassword);
//        if (hashResult.IsFailure)
//            return hashResult;

//        PasswordHash = hashResult.Value;
//        SecurityStamp = Guid.NewGuid().ToString(); // Invalidate previous tokens
//        return Result.Success();
//    }
//}

//// Domain/Enums/RoleType.cs
//public enum RoleType
//{
//    [Description("Системный администратор")]
//    SystemAdmin = 1,

//    [Description("Директор учебного заведения")]
//    SchoolDirector,

//    [Description("Заведующий учебной частью")]
//    AcademicAffairsHead,

//    [Description("Преподаватель")]
//    Teacher,

//    [Description("Куратор группы")]
//    GroupCurator,

//    [Description("Студент")]
//    Student,

//    [Description("Родитель")]
//    Parent,

//    [Description("Методист")]
//    EducationMethodist,

//    [Description("Финансовый менеджер")]
//    FinancialManager,

//    [Description("Менеджер по качеству")]
//    QualityAssuranceManager,

//    [Description("Контент-менеджер")]
//    ContentManager,

//    [Description("Аналитик")]
//    DataAnalyst,

//    [Description("Гость")]
//    Guest
//}

//// Domain/Entities/Identity/UserRole.cs
//public class UserRole : Entity
//{
//    public Guid UserUid { get; private set; }
//    public RoleType Role { get; private set; }
//    public DateTime AssignedAtUtc { get; private set; }
//    public Guid? AssignedByUserUid { get; private set; }
//    public Guid? ScopeId { get; private set; } // Для ограничения прав определенной организацией/группой

//    private UserRole() { }

//    public static Result<UserRole> Create(Guid userUid, RoleType role, Guid? assignedBy = null)
//    {
//        return new UserRole
//        {
//            Uid = Guid.NewGuid(),
//            UserUid = userUid,
//            Role = role,
//            AssignedAtUtc = DateTime.UtcNow,
//            AssignedByUserUid = assignedBy
//        };
//    }
//}

//// Domain/Entities/Education/Course.cs
//public class Course : Entity
//{
//    public Guid Uid { get; private set; }
//    public string Code { get; private set; }
//    public string Name { get; private set; }
//    public string Description { get; private set; }
//    public int Credits { get; private set; }
//    public DateTime StartDate { get; private set; }
//    public DateTime EndDate { get; private set; }
//    public CourseStatus Status { get; private set; }

//    // Relationships
//    public Guid DepartmentUid { get; private set; }
//    public Guid? PrimaryInstructorUid { get; private set; }

//    private readonly List<CourseModule> _modules = new();
//    public IReadOnlyCollection<CourseModule> Modules => _modules.AsReadOnly();

//    // Methods
//    public Result UpdateStatus(CourseStatus newStatus)
//    {
//        // Validation logic
//        Status = newStatus;
//        Raise(new CourseStatusChangedDomainEvent(Uid, newStatus));
//        return Result.Success();
//    }
//}

//// Domain/Enums/CourseStatus.cs
//public enum CourseStatus
//{
//    [Description("В разработке")]
//    Draft,

//    [Description("На утверждении")]
//    UnderReview,

//    [Description("Активный")]
//    Active,

//    [Description("Архивированный")]
//    Archived,

//    [Description("Приостановлен")]
//    Suspended
//}
//// Infrastructure/Auth/PermissionRequirement.cs
//public class PermissionRequirement : IAuthorizationRequirement
//{
//    public static readonly string PolicyPrefix = "PERMISSION_";

//    public PermissionType Permission { get; }

//    public PermissionRequirement(PermissionType permission)
//    {
//        Permission = permission;
//    }
//}

//// Domain/Enums/PermissionType.cs
//public enum PermissionType
//{
//    [Description("Просмотр оценок")]
//    ViewGrades,

//    [Description("Редактирование оценок")]
//    EditGrades,

//    [Description("Управление курсами")]
//    ManageCourses,

//    [Description("Доступ к финансовым данным")]
//    ViewFinancialData,

//    [Description("Генерация отчетов")]
//    GenerateReports,

//    [Description("Управление пользователями")]
//    ManageUsers
//}

//// Infrastructure/Auth/PermissionPolicyProvider.cs
//// Реализация динамической проверки прав через политики

//// Domain/Entities/Education/Assignment.cs
//public class Assignment : Entity
//{
//    public Guid Uid { get; private set; }
//    public string Title { get; private set; }
//    public string Description { get; private set; }
//    public DateTime DueDateUtc { get; private set; }
//    public int MaxPoints { get; private set; }
//    public AssignmentType Type { get; private set; }

//    public Guid CourseUid { get; private set; }
//    public Guid CreatedByUid { get; private set; }

//    private readonly List<Submission> _submissions = new();
//    public IReadOnlyCollection<Submission> Submissions => _submissions.AsReadOnly();
//}

//// Domain/Enums/AssignmentType.cs
//public enum AssignmentType
//{
//    [Description("Домашнее задание")]
//    Homework,

//    [Description("Проект")]
//    Project,

//    [Description("Экзамен")]
//    Exam,

//    [Description("Тест")]
//    Quiz
//}

//// Domain/Entities/Communication/Notification.cs
//public class Notification : Entity
//{
//    public Guid Uid { get; private set; }
//    public string Title { get; private set; }
//    public string Message { get; private set; }
//    public NotificationType Type { get; private set; }
//    public DateTime SentAtUtc { get; private set; }
//    public bool IsRead { get; private set; }

//    public Guid RecipientUid { get; private set; }
//    public Guid? SenderUid { get; private set; }
//}