using Common.Data.Domain;
using MediatR;

namespace Common.Core.CQRS.Queries
{
    public class GetUserByIdQuery : IRequest<Common.Data.Domain.User>
    {
        public int ID { get; set; }
    }
}
