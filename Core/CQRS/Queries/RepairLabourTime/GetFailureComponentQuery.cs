using Common.Core.Models.RepairLabourTimeModels;
using MediatR;

namespace Common.Core.CQRS.Queries.RepairLabourTime
{
    public class GetFailureComponentQuery : IRequest<FailureComponentModel>
    {
        public int Id { get; set; }
    }
}
