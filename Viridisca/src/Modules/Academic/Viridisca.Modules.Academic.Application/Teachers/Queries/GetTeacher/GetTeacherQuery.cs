using System;
using MediatR;
using Viridisca.Modules.Academic.Application.Teachers.Queries.GetTeacher.Dto;

namespace Viridisca.Modules.Academic.Application.Teachers.Queries.GetTeacher
{
    public sealed record GetTeacherQuery(Guid TeacherUid) : IRequest<TeacherDto>;
} 