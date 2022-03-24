using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core.CQRS.Queries.User
{
    public class DoesUserBelongToOrganisationQuery : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public int OrganizationId { get; set; }
    }
}
