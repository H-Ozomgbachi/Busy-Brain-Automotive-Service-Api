using Common.Core.Models;
using MediatR;
using System.Collections.Generic;


namespace Common.Core.CQRS.Queries
{
    public class GetOrganisationsQuery : IRequest<List<ListItemViewModel>>
    {

    }
}
