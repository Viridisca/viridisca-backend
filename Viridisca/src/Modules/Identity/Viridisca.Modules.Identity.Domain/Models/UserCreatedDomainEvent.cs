using System;
using Viridisca.Common.Domain;

namespace Viridisca.Modules.Identity.Domain.Models;

public class UserCreatedDomainEvent(Guid userUid) : DomainEvent
{
    public Guid UserUid { get; } = userUid;
}
