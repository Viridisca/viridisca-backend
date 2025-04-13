using Microsoft.AspNetCore.Routing;

namespace Viridisca.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
