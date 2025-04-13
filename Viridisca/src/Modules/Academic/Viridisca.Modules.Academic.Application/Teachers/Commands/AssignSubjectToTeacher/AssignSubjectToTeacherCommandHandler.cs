using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Viridisca.Common.Application.Data;
using Viridisca.Modules.Academic.Domain.Models;

namespace Viridisca.Modules.Academic.Application.Teachers.Commands.AssignSubjectToTeacher
{
    internal sealed class AssignSubjectToTeacherCommandHandler : IRequestHandler<AssignSubjectToTeacherCommand, Guid>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignSubjectToTeacherCommandHandler(
            ITeacherRepository teacherRepository,
            ISubjectRepository subjectRepository,
            IUnitOfWork unitOfWork)
        {
            _teacherRepository = teacherRepository;
            _subjectRepository = subjectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(AssignSubjectToTeacherCommand request, CancellationToken cancellationToken)
        {
            // Получаем преподавателя
            var teacher = await _teacherRepository.GetByUidAsync(request.TeacherUid, cancellationToken);
            if (teacher == null)
            {
                throw new Exception($"Преподаватель с ID {request.TeacherUid} не найден");
            }

            // Проверяем, существует ли предмет
            var subject = await _subjectRepository.GetByUidAsync(request.SubjectUid, cancellationToken);
            if (subject == null)
            {
                throw new Exception($"Предмет с ID {request.SubjectUid} не найден");
            }

            // Проверяем, не назначен ли уже этот предмет преподавателю
            if (teacher.Subjects.Any(s => s.SubjectUid == request.SubjectUid && s.IsActive))
            {
                throw new Exception($"Предмет с ID {request.SubjectUid} уже назначен этому преподавателю");
            }

            // Если этот преподаватель становится основным преподавателем предмета,
            // то для других преподавателей нужно сбросить этот флаг
            if (request.IsMainTeacher)
            {
                // Это должно происходить в отдельном сервисе или доменном событии,
                // но для упрощения предположим, что это происходит здесь
            }

            // Создаем связь преподавателя с предметом
            var teacherSubject = TeacherSubject.Create(
                request.TeacherUid,
                request.SubjectUid,
                request.IsMainTeacher);

            // Добавляем предмет преподавателю
            teacher.AddSubject(teacherSubject);
            _teacherRepository.Update(teacher);

            // Сохраняем изменения
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return teacherSubject.Uid;
        }
    }
} 