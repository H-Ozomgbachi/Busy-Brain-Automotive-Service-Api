using Common.Core.Models;
using MediatR;
using System.Collections.Generic;


namespace Common.Core.CQRS.Queries
{
    public class GetOrganisationQuery : IRequest<OrganisationViewModel>
    {
        public int Id { get; set; }
    }
}
