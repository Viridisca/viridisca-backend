using Viridisca.Common.Domain;

namespace Viridisca.Modules.Lessons.Domain.Lessons;

public static class LessonErrors
{
    public static Error NotFound(Guid lessonId) =>
        Error.NotFound("Lessons.NotFound", $"The lesson with the identifier {lessonId} was not found");

    public static readonly Error StartDateInPast = Error.Problem(
        "Lessons.StartDateInPast",
        "The lesson start date is in the past");

    public static readonly Error EndDatePrecedesStartDate = Error.Problem(
        "Lessons.EndDatePrecedesStartDate",
        "The lesson end date precedes the start date");

    public static readonly Error NoTicketsFound = Error.Problem(
        "Lessons.NoTicketsFound",
        "The lesson does not have any ticket types defined");

    public static readonly Error NotDraft = Error.Problem("Lessons.NotDraft", "The lesson is not in draft status");


    public static readonly Error AlreadyCanceled = Error.Problem(
        "Lessons.AlreadyCanceled",
        "The lesson was already canceled");


    public static readonly Error AlreadyStarted = Error.Problem(
        "Lessons.AlreadyStarted",
        "The lesson has already started");
}
