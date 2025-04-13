using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Viridisca.Common.Presentation.Endpoints;
using Viridisca.Modules.Grading.Application.Revisions.Queries.GetGradeRevisions;
using Viridisca.Modules.Grading.Application.Revisions.Queries.GetGradeRevisions.DTO;

namespace Viridisca.Modules.Grading.Presentation.Endpoints.Revisions
{
    internal sealed class GetGradeRevisionsEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapGet("/api/grading/grades/{gradeUid:guid}/revisions", HandleAsync)
                .WithName("GetGradeRevisions")
                .WithTags("GradeRevisions")
                .RequireAuthorization();
        }

        private static async Task<IResult> HandleAsync(
            Guid gradeUid, 
            IMediator mediator, 
            CancellationToken cancellationToken)
        {
            GetGradeRevisionsQuery query = new() { GradeUid = gradeUid };
            
            IReadOnlyList<GradeRevisionDto> revisions = await mediator.Send(query, cancellationToken);
            
            return Results.Ok(revisions);
        }
    }
} 