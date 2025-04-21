using System;
using MediatR;

namespace Viridisca.Modules.Identity.Application.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(
    Guid UserUid,
    string FirstName,
    string LastName,
    string MiddleName,
    string PhoneNumber) : IRequest<bool>;
