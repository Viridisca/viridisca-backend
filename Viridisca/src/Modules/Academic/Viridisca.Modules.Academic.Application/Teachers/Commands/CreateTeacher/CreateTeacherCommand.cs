using System;
using MediatR;

namespace Viridisca.Modules.Academic.Application.Teachers.Commands.CreateTeacher
{
    public sealed record CreateTeacherCommand(
        Guid UserUid,
        string EmployeeCode,
        DateTime HireDate,
        string Specialization,
        string Qualifications,
        int YearsOfExperience,
        Guid? DepartmentUid = null) : IRequest<Guid>;
}