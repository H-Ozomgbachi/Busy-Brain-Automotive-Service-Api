using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core.CQRS.Commands.User
{
    public class ModifyUserRoleCommand : IRequest<int>
    {
        public string[] Roles { get; set; }
        public Guid UniqueId { get; set; }
    }
}
