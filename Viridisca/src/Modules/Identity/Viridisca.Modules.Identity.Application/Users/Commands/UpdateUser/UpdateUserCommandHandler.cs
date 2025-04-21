using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Viridisca.Common.Application.Data;
using Viridisca.Modules.Identity.Domain.Models;
using Viridisca.Modules.Identity.Domain.Repositories;

namespace Viridisca.Modules.Identity.Application.Users.Commands.UpdateUser;

internal sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUidAsync(request.UserUid, cancellationToken);

        if (user == null)
        {
            throw new Exception($"Пользователь с ID {request.UserUid} не найден");
        }

        user.UpdatePersonalInfo(
            request.FirstName,
            request.LastName,
            request.MiddleName,
            request.PhoneNumber);

        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
