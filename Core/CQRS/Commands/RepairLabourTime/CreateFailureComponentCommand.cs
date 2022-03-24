using MediatR;
using System;

namespace Common.Core.CQRS.Commands.RepairLabourTime
{
    public class CreateFailureComponentCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string AssemblyOrSystemName { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
