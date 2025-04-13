using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Viridisca.Common.Application.Data;
using Viridisca.Modules.Academic.Application.Common.Interfaces;
using Viridisca.Modules.Academic.Domain.Models;

namespace Viridisca.Modules.Academic.Application.Teachers.Commands.CreateTeacher
{
    internal sealed class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, Guid>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserInfoService _userInfoService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTeacherCommandHandler(
            ITeacherRepository teacherRepository,
            IUserInfoService userInfoService,
            IUnitOfWork unitOfWork)
        {
            _teacherRepository = teacherRepository;
            _userInfoService = userInfoService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            // Проверяем, существует ли пользователь
            var userExists = await _userInfoService.UserExistsAsync(request.UserUid, cancellationToken);
            if (!userExists)
            {
                throw new Exception($"Пользователь с ID {request.UserUid} не найден");
            }

            // Проверяем, что преподавателя с таким кодом сотрудника еще нет
            var exists = await _teacherRepository.ExistsByEmployeeCodeAsync(request.EmployeeCode, cancellationToken);
            if (exists)
            {
                throw new Exception($"Преподаватель с кодом {request.EmployeeCode} уже существует");
            }

            // Создаем преподавателя
            var teacherResult = Teacher.Create(
                request.UserUid,
                request.EmployeeCode,
                request.HireDate,
                request.Specialization,
                request.Qualifications,
                request.YearsOfExperience,
                request.DepartmentUid);

            if (teacherResult.IsFailure)
            {
                throw new Exception(teacherResult.Error.Message);
            }

            var teacher = teacherResult.Value;

            // Сохраняем преподавателя
            _teacherRepository.Insert(teacher);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return teacher.Uid;
        }
    }
} 