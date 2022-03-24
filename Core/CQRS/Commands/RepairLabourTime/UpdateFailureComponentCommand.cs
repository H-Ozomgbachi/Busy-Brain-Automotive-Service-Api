using MediatR;
using System;

namespace Common.Core.CQRS.Commands.RepairLabourTime
{
    public class UpdateFailureComponentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AssemblyOrSystemName { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
