using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Viridisca.Modules.Academic.Application.Common.Interfaces;
using Viridisca.Modules.Academic.Application.Teachers.Queries.GetTeacher.Dto;
using Viridisca.Modules.Academic.Domain.Models;

namespace Viridisca.Modules.Academic.Application.Teachers.Queries.GetTeacher
{
    internal sealed class GetTeacherQueryHandler : IRequestHandler<GetTeacherQuery, TeacherDto>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserInfoService _userInfoService;

        public GetTeacherQueryHandler(
            ITeacherRepository teacherRepository,
            IUserInfoService userInfoService)
        {
            _teacherRepository = teacherRepository;
            _userInfoService = userInfoService;
        }

        public async Task<TeacherDto> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetByUidAsync(request.TeacherUid, cancellationToken);

            if (teacher == null)
            {
                throw new Exception($"Преподаватель с ID {request.TeacherUid} не найден");
            }

            // Получаем базовую информацию о пользователе из модуля Identity
            var userInfo = await _userInfoService.GetUserInfoAsync(teacher.UserUid, cancellationToken);

            // Создаем DTO преподавателя
            var teacherDto = new TeacherDto
            {
                Uid = teacher.Uid,
                UserUid = teacher.UserUid,
                EmployeeCode = teacher.EmployeeCode,
                HireDate = teacher.HireDate,
                Status = teacher.Status.ToString(),
                Specialization = teacher.Specialization,
                Qualifications = teacher.Qualifications,
                YearsOfExperience = teacher.YearsOfExperience,
                Biography = teacher.Biography,
                DepartmentUid = teacher.DepartmentUid,
                CreatedAtUtc = teacher.CreatedAtUtc,
                
                // Данные из модуля Identity
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                MiddleName = userInfo.MiddleName,
                Email = userInfo.Email,
                PhoneNumber = userInfo.PhoneNumber,
                ProfileImageUrl = userInfo.ProfileImageUrl,
                
                // Преподаваемые предметы
                Subjects = teacher.Subjects.Select(s => new TeacherSubjectDto
                {
                    Uid = s.Uid,
                    SubjectUid = s.SubjectUid,
                    // Здесь нужно было бы получить названия предметов из репозитория предметов
                    // но для упрощения оставим только идентификаторы
                    IsMainTeacher = s.IsMainTeacher,
                    AssignedAtUtc = s.AssignedAtUtc,
                    EndedAtUtc = s.EndedAtUtc,
                    IsActive = s.IsActive
                }).ToList(),
                
                // Группы преподавателя
                Groups = teacher.Groups.Select(g => new TeacherGroupDto
                {
                    Uid = g.Uid,
                    GroupUid = g.GroupUid,
                    SubjectUid = g.SubjectUid,
                    IsCurator = g.IsCurator,
                    AssignedAtUtc = g.AssignedAtUtc,
                    EndedAtUtc = g.EndedAtUtc,
                    IsActive = g.IsActive
                }).ToList()
            };

            return teacherDto;
        }
    }
} 