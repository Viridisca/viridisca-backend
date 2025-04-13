using Viridisca.Modules.Lessons.Application.Lessons.CreateLesson;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MediatR; 
using Viridisca.Common.Presentation.Endpoints;

namespace Viridisca.Modules.Lessons.Presentation.Lessons;

internal sealed class CreateLesson : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("lessons", async (Request request, ISender sender) =>
        {
            var command = new CreateLessonCommand(
               request.Subject,
               request.Description,
               request.Classroom,
               request.StartTime,
               request.EndTime,
               request.GroupUid,
               request.TeacherUid);

            Guid lessonId = await sender.Send(command);
            return Results.Ok(lessonId);
        })
        .WithTags(Tags.Lessons);
    }

    internal sealed class Request
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Classroom { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid GroupUid { get; set; }
        public Guid TeacherUid { get; set; }
    }
}
