using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Viridisca.Common.Application.Data;
using Viridisca.Modules.Academic.Domain.Models;

namespace Viridisca.Modules.Academic.Application.Students.Commands.UpdateStudent
{
    internal sealed class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, bool>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStudentCommandHandler(
            IStudentRepository studentRepository,
            IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByUidAsync(request.StudentUid, cancellationToken);

            if (student == null)
            {
                throw new Exception($"Студент с ID {request.StudentUid} не найден");
            }

            // Обновляем контактную информацию
            student.UpdateEmergencyContact(request.EmergencyContactName, request.EmergencyContactPhone);
            
            // Обновляем медицинскую информацию
            student.UpdateMedicalInformation(request.MedicalInformation);

            // Сохраняем изменения
            _studentRepository.Update(student);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
} 