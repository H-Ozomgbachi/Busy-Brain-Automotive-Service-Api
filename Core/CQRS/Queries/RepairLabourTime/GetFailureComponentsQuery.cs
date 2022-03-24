using Common.Core.Models;
using Common.Core.Models.RepairLabourTimeModels;
using MediatR;

namespace Common.Core.CQRS.Queries.RepairLabourTime
{
    public class GetFailureComponentsQuery : IRequest<PagedListResult<FailureComponentModel>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
