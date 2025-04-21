using Microsoft.AspNetCore.Routing;

namespace Viridisca.Common.Presentation.Endpoints;

/// <summary>
/// Interface for endpoint definition
/// </summary>
public interface IEndpoint
{
    /// <summary>
    /// Maps the endpoint to the endpoint route builder
    /// </summary>
    /// <param name="app">The endpoint route builder</param>
    void MapEndpoint(IEndpointRouteBuilder app);
}
