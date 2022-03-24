using MediatR;
using System;

namespace Common.Core.CQRS.Queries
{
    public class DoesUserEmailExistQuery : IRequest<bool>
    {
        public string Email { get; set; }
        public Guid ExcludeUserId { get; set; }
    }
}
