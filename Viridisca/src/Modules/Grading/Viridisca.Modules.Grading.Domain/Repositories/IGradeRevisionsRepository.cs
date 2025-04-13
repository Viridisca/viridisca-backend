using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Viridisca.Modules.Grading.Domain.Models;

namespace Viridisca.Modules.Grading.Domain.Repositories
{
    public interface IGradeRevisionsRepository
    {
        /// <summary>
        /// Получает все ревизии для указанной оценки
        /// </summary>
        /// <param name="gradeUid">Идентификатор оценки</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список ревизий оценки</returns>
        Task<IReadOnlyList<GradeRevision>> GetRevisionsForGradeAsync(Guid gradeUid, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Добавляет новую ревизию оценки
        /// </summary>
        /// <param name="revision">Ревизия для добавления</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task AddRevisionAsync(GradeRevision revision, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Получает последнюю ревизию для указанной оценки
        /// </summary>
        /// <param name="gradeUid">Идентификатор оценки</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Последняя ревизия оценки или null</returns>
        Task<GradeRevision> GetLatestRevisionForGradeAsync(Guid gradeUid, CancellationToken cancellationToken = default);
    }
} 