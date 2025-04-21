using FluentValidation.Results;

namespace Viridisca.Common.Application.Identity;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
        Errors = new Dictionary<string, string[]>
        {
            { "Error", new[] { message } }
        };
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : base("One or more validation failures have occurred.")
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}