using Common.Data.Domain;
using MediatR;


namespace Common.Core.CQRS.Queries
{
    public class GetUserByUsernameQuery : IRequest<Common.Data.Domain.User>
    {
        public string Username { get; set; }
    }
}
