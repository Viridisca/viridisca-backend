using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Viridisca.Common.Application.Data;
using Viridisca.Modules.Academic.Domain.Models;

namespace Viridisca.Modules.Academic.Application.Students.Commands.CreateStudent
{
    internal sealed class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateStudentCommandHandler(
            IStudentRepository studentRepository,
            IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            // Проверяем, что студента с таким кодом еще нет
            var exists = await _studentRepository.ExistsByStudentCodeAsync(request.StudentCode, cancellationToken);
            if (exists)
            {
                throw new Exception($"Студент с кодом {request.StudentCode} уже существует");
            }

            // Создаем студента
            var studentResult = Student.Create(
                request.UserUid,
                request.FirstName,
                request.LastName,
                request.Email,
                request.BirthDate,
                request.MiddleName,
                request.PhoneNumber,
                request.GroupUid);

            if (studentResult.IsFailure)
            {
                throw new Exception(studentResult.Error.Message);
            }

            var student = studentResult.Value;

            // Устанавливаем дополнительные свойства
            if (!string.IsNullOrEmpty(request.StudentCode))
            {
                student.UpdateStudentCode(request.StudentCode);
            }
            
            if (request.EnrollmentDate != default)
            {
                student.UpdateEnrollmentDate(request.EnrollmentDate);
            }

            // Сохраняем студента
            _studentRepository.Insert(student);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return student.Uid;
        }
    }
} 