using MediatR;

namespace Common.Core.CQRS.Queries.User
{
    public class CountUsersInOrganisationQuery : IRequest<int>
    {
        public int OrganisationId { get; set; }
    }
}
