using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Viridisca.Common.Presentation.Endpoints;
using Viridisca.Modules.Grading.Application.Revisions.Commands.CreateGradeRevision;

namespace Viridisca.Modules.Grading.Presentation.Endpoints.Revisions
{
    internal sealed class CreateGradeRevisionEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapPost("/api/grading/grades/{gradeUid:guid}/revisions", HandleAsync)
                .WithName("CreateGradeRevision")
                .WithTags("GradeRevisions")
                .RequireAuthorization();
        }

        private static async Task<IResult> HandleAsync(
            Guid gradeUid,
            CreateGradeRevisionRequest request,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            CreateGradeRevisionCommand command = new()
            {
                GradeUid = gradeUid,
                TeacherUid = request.TeacherUid,
                PreviousValue = request.PreviousValue,
                NewValue = request.NewValue,
                PreviousDescription = request.PreviousDescription,
                NewDescription = request.NewDescription,
                RevisionReason = request.RevisionReason
            };

            await mediator.Send(command, cancellationToken);

            return Results.Created($"/api/grading/grades/{gradeUid}/revisions", null);
        }
    }

    public class CreateGradeRevisionRequest
    {
        public Guid TeacherUid { get; init; }
        public decimal PreviousValue { get; init; }
        public decimal NewValue { get; init; }
        public string PreviousDescription { get; init; }
        public string NewDescription { get; init; }
        public string RevisionReason { get; init; }
    }
} 