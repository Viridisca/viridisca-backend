using Viridisca.Common.Domain;

namespace Viridisca.Common.Application.Exceptions;

public sealed class ViridiscaException(
    string requestName, 
    Error? error = default, 
    Exception? innerException = default) : Exception("Application exception", innerException)
{
    public string RequestName { get; } = requestName;

    public Error? Error { get; } = error;
}
