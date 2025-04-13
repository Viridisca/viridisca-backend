using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Viridisca.Common.Application.Data;
using Viridisca.Modules.Academic.Application.Common.Interfaces;
using Viridisca.Modules.Academic.Domain.Models;

namespace Viridisca.Modules.Academic.Application.Students.Commands.AddStudentParent
{
    internal sealed class AddStudentParentCommandHandler : IRequestHandler<AddStudentParentCommand, Guid>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserInfoService _userInfoService;
        private readonly IUnitOfWork _unitOfWork;

        public AddStudentParentCommandHandler(
            IStudentRepository studentRepository,
            IUserInfoService userInfoService,
            IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _userInfoService = userInfoService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(AddStudentParentCommand request, CancellationToken cancellationToken)
        {
            // Получаем студента
            var student = await _studentRepository.GetByUidAsync(request.StudentUid, cancellationToken);
            if (student == null)
            {
                throw new Exception($"Студент с ID {request.StudentUid} не найден");
            }

            // Проверяем, существует ли пользователь-родитель
            var parentExists = await _userInfoService.UserExistsAsync(request.ParentUserUid, cancellationToken);
            if (!parentExists)
            {
                throw new Exception($"Пользователь с ID {request.ParentUserUid} не найден");
            }

            // Если данный родитель устанавливается как основной контакт,
            // то необходимо сбросить флаг у других родителей
            if (request.IsPrimaryContact)
            {
                foreach (var parent in student.Parents)
                {
                    if (parent.IsPrimaryContact)
                    {
                        parent.SetAsPrimaryContact(false);
                    }
                }
            }

            // Создаем связь студента с родителем
            var studentParent = StudentParent.Create(
                request.StudentUid,
                request.ParentUserUid,
                request.Relation,
                request.IsPrimaryContact,
                request.HasLegalGuardianship);

            // Добавляем родителя к студенту
            student.AddParent(studentParent);
            _studentRepository.Update(student);

            // Сохраняем изменения
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return studentParent.Uid;
        }
    }
} 