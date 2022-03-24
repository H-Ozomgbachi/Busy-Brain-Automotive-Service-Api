using MediatR;
using System;

namespace Common.Core.CQRS.Commands.RepairLabourTime
{
    public class CreateMaintenanceItemCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public int LabourTimeHours { get; set; }
        public string TruckModel { get; set; }
        public decimal CostPerHour { get; set; }
        public int FailureComponentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
