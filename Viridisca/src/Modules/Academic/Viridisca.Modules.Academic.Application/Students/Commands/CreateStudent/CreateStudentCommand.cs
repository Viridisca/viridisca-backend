using System;
using MediatR;

namespace Viridisca.Modules.Academic.Application.Students.Commands.CreateStudent
{
    public sealed record CreateStudentCommand(
        Guid UserUid,
        string FirstName,
        string LastName,
        string Email,
        DateTime BirthDate,
        string StudentCode,
        DateTime EnrollmentDate,
        string MiddleName = null,
        string PhoneNumber = null,
        Guid? GroupUid = null) : IRequest<Guid>;
} 