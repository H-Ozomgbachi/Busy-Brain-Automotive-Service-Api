using Common.Data.Domain;
using MediatR;
using System;

namespace Common.Core.CQRS.Queries
{
    public class GetUserByGuidQuery : IRequest<Common.Data.Domain.User>
    {
        public Guid ID { get; set; }
    }
}
