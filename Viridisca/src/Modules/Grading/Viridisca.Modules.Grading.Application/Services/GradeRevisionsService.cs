using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Viridisca.Modules.Grading.Domain.Models;
using Viridisca.Modules.Grading.Domain.Repositories;

namespace Viridisca.Modules.Grading.Application.Services
{
    public class GradeRevisionsService
    {
        private readonly IGradeRevisionsRepository _revisionsRepository;

        public GradeRevisionsService(IGradeRevisionsRepository revisionsRepository)
        {
            _revisionsRepository = revisionsRepository ?? throw new ArgumentNullException(nameof(revisionsRepository));
        }

        /// <summary>
        /// Получает все ревизии для указанной оценки
        /// </summary>
        /// <param name="gradeUid">Идентификатор оценки</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список ревизий</returns>
        public async Task<IReadOnlyList<GradeRevision>> GetRevisionsForGradeAsync(
            Guid gradeUid, 
            CancellationToken cancellationToken = default)
        {
            return await _revisionsRepository.GetRevisionsForGradeAsync(gradeUid, cancellationToken);
        }

        /// <summary>
        /// Получает последнюю ревизию для указанной оценки
        /// </summary>
        /// <param name="gradeUid">Идентификатор оценки</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Последняя ревизия или null, если ревизий нет</returns>
        public async Task<GradeRevision> GetLatestRevisionAsync(
            Guid gradeUid, 
            CancellationToken cancellationToken = default)
        {
            return await _revisionsRepository.GetLatestRevisionForGradeAsync(gradeUid, cancellationToken);
        }

        /// <summary>
        /// Создает новую ревизию оценки
        /// </summary>
        /// <param name="grade">Оценка</param>
        /// <param name="previousValue">Предыдущее значение оценки</param>
        /// <param name="previousDescription">Предыдущее описание оценки</param>
        /// <param name="newValue">Новое значение оценки</param>
        /// <param name="newDescription">Новое описание оценки</param>
        /// <param name="teacherUid">Идентификатор преподавателя, выполнившего изменение</param>
        /// <param name="revisionReason">Причина изменения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Задача, представляющая асинхронную операцию</returns>
        public async Task CreateRevisionAsync(
            Grade grade,
            decimal previousValue,
            string previousDescription,
            decimal newValue,
            string newDescription,
            Guid teacherUid,
            string revisionReason,
            CancellationToken cancellationToken = default)
        {
            if (grade == null)
                throw new ArgumentNullException(nameof(grade));

            if (teacherUid == Guid.Empty)
                throw new ArgumentException("Teacher UID cannot be empty", nameof(teacherUid));

            var revision = new GradeRevision(
                Guid.NewGuid(),
                grade.Uid,
                teacherUid,
                previousValue,
                newValue,
                previousDescription,
                newDescription,
                revisionReason,
                DateTime.UtcNow);

            await _revisionsRepository.AddRevisionAsync(revision, cancellationToken);
        }
    }
} 