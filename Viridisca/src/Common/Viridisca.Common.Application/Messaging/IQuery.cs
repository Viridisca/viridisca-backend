using Viridisca.Common.Domain;
using MediatR;

namespace Viridisca.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
