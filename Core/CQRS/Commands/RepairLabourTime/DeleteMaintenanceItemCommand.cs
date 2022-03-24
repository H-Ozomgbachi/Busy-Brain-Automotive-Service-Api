using MediatR;

namespace Common.Core.CQRS.Commands.RepairLabourTime
{
    public class DeleteMaintenanceItemCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int FailureComponentId { get; set; }
    }
}