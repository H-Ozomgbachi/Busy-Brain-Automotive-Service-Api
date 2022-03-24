using Common.Core.Models.RepairLabourTimeModels;
using MediatR;
using System.Collections.Generic;

namespace Common.Core.CQRS.Queries.RepairLabourTime
{
    public class GetMaintenanceItemsQuery : IRequest<IEnumerable<MaintenanceItemModel>>
    {
        public int? FailureComponentId { get; set; }
        public List<int> FailureComponentIds { get; set; }
    }
}
