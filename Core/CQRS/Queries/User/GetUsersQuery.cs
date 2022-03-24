using Common.Core.Models;
using MediatR;
using System.Collections.Generic;

namespace Common.Core.CQRS.Queries.User
{
    public class GetUsersQuery : IRequest<List<ListUserViewModel>>
    {

    }
}
