using MediatR;

namespace Viridisca.Modules.Identity.Application.Users.Commands;

public record ChangePasswordCommand(
    string CurrentPassword,
    string NewPassword,
    string ConfirmPassword) : IRequest; 