using Viridisca.Modules.Lessons.Domain.Lessons;
using MediatR;

namespace Viridisca.Modules.Lessons.Application.Lessons.GetLesson;

internal sealed class GetLessonQueryHandler(
    // IDbConnectionFactory dbConnectionFactory
    ILessonRepository lessonRepository)
    : IRequestHandler<GetLessonQuery, LessonResponse?>
{
    public async Task<LessonResponse?> Handle(GetLessonQuery request, CancellationToken cancellationToken)
    {
        //await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        //const string sql =
        //    $"""
        //     SELECT
        //         id AS {nameof(LessonResponse.Id)},
        //         title AS {nameof(LessonResponse.Title)},
        //         description AS {nameof(LessonResponse.Description)},
        //         location AS {nameof(LessonResponse.Location)},
        //         starts_at_utc AS {nameof(LessonResponse.StartsAtUtc)},
        //         ends_at_utc AS {nameof(LessonResponse.EndsAtUtc)}
        //     FROM events.events
        //     WHERE id = @LessonId
        //     """;

        // LessonResponse? @event = await connection.QuerySingleOrDefaultAsync<LessonResponse>(sql, request);

        Lesson? lesson = await lessonRepository.GetAsync(request.LessonId, cancellationToken);

        if (lesson is null)
        {
            return null;
        }

        return new LessonResponse(
            lesson.Uid,
            lesson.Subject,
            lesson.Description,
            lesson.Classroom,
            lesson.StartsAtUtc,
            lesson.EndsAtUtc,
            lesson.GroupUid,
            lesson.TeacherUid);
        ;
    }
}
