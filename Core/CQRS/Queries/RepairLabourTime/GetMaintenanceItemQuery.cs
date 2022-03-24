using Common.Core.Models.RepairLabourTimeModels;
using MediatR;

namespace Common.Core.CQRS.Queries.RepairLabourTime
{
    public class GetMaintenanceItemQuery : IRequest<MaintenanceItemModel>
    {
        public int Id { get; set; }
        public int FailureComponentId { get; set; }
    }
}
