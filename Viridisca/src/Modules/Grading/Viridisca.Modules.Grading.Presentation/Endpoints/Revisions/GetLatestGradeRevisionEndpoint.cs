using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Viridisca.Common.Presentation.Endpoints;
using Viridisca.Modules.Grading.Application.Revisions.Queries.GetLatestGradeRevision;

namespace Viridisca.Modules.Grading.Presentation.Endpoints.Revisions
{
    internal sealed class GetLatestGradeRevisionEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapGet("/api/grading/grades/{gradeUid:guid}/revisions/latest", HandleAsync)
                .WithName("GetLatestGradeRevision")
                .WithTags("GradeRevisions")
                .RequireAuthorization();
        }

        private static async Task<IResult> HandleAsync(
            Guid gradeUid, 
            IMediator mediator, 
            CancellationToken cancellationToken)
        {
            GetLatestGradeRevisionQuery query = new() { GradeUid = gradeUid };
            
            var revision = await mediator.Send(query, cancellationToken);
            
            if (revision == null)
                return Results.NotFound();
                
            return Results.Ok(revision);
        }
    }
} 