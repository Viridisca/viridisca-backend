using System;
using MediatR;
using Viridisca.Modules.Academic.Application.Students.Queries.GetStudent.Dto;

namespace Viridisca.Modules.Academic.Application.Students.Queries.GetStudent
{
    public sealed record GetStudentQuery(Guid StudentUid) : IRequest<StudentDto>;
} 