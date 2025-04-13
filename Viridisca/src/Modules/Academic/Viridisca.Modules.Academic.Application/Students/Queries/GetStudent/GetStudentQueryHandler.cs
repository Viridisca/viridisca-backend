using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Viridisca.Modules.Academic.Application.Students.Queries.GetStudent.Dto;
using Viridisca.Modules.Academic.Domain.Models;
using Viridisca.Modules.Academic.Application.Common.Interfaces;
using Viridisca.Modules.Academic.Domain.Groups;

namespace Viridisca.Modules.Academic.Application.Students.Queries.GetStudent
{
    internal sealed class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserInfoService _userInfoService;

        public GetStudentQueryHandler(
            IStudentRepository studentRepository,
            IGroupRepository groupRepository,
            IUserInfoService userInfoService)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
            _userInfoService = userInfoService;
        }

        public async Task<StudentDto> Handle(GetStudentQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByUidAsync(request.StudentUid, cancellationToken);

            if (student == null)
            {
                throw new Exception($"Студент с ID {request.StudentUid} не найден");
            }

            // Получаем информацию о группе, если студент привязан к группе
            string groupName = null;
            if (student.GroupUid.HasValue)
            {
                var group = await _groupRepository.GetByUidAsync(student.GroupUid.Value, cancellationToken);
                groupName = group?.Name;
            }

            // Получаем базовую информацию о пользователе из модуля Identity
            var userInfo = await _userInfoService.GetUserInfoAsync(student.UserUid, cancellationToken);

            // Создаем DTO студента
            var studentDto = new StudentDto
            {
                Uid = student.Uid,
                UserUid = student.UserUid,
                StudentCode = student.StudentCode,
                EnrollmentDate = student.EnrollmentDate,
                Status = student.Status.ToString(),
                GroupUid = student.GroupUid,
                GroupName = groupName,
                EmergencyContactName = student.EmergencyContactName,
                EmergencyContactPhone = student.EmergencyContactPhone,
                MedicalInformation = student.MedicalInformation,
                GraduationDate = student.GraduationDate,
                CreatedAtUtc = student.CreatedAtUtc,
                
                // Данные из модуля Identity
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                MiddleName = userInfo.MiddleName,
                Email = userInfo.Email,
                PhoneNumber = userInfo.PhoneNumber,
                ProfileImageUrl = userInfo.ProfileImageUrl,
                DateOfBirth = userInfo.DateOfBirth,
                
                // Данные о родителях
                Parents = student.Parents.Select(p => new StudentParentDto
                {
                    Uid = p.Uid,
                    ParentUserUid = p.ParentUserUid,
                    ParentFullName = _userInfoService.GetUserFullNameAsync(p.ParentUserUid, cancellationToken).Result,
                    Relation = p.Relation.ToString(),
                    IsPrimaryContact = p.IsPrimaryContact,
                    HasLegalGuardianship = p.HasLegalGuardianship,
                    Email = _userInfoService.GetUserEmailAsync(p.ParentUserUid, cancellationToken).Result,
                    PhoneNumber = _userInfoService.GetUserPhoneAsync(p.ParentUserUid, cancellationToken).Result
                }).ToList()
            };

            return studentDto;
        }
    }
} 