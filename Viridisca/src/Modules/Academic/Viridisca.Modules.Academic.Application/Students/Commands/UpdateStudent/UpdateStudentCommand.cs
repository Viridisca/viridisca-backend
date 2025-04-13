using System;
using MediatR;

namespace Viridisca.Modules.Academic.Application.Students.Commands.UpdateStudent
{
    public sealed record UpdateStudentCommand(
        Guid StudentUid,
        string EmergencyContactName,
        string EmergencyContactPhone,
        string MedicalInformation) : IRequest<bool>;
} 