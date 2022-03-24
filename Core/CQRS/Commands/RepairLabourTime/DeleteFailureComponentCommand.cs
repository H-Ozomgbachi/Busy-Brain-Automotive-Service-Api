using MediatR;

namespace Common.Core.CQRS.Commands.RepairLabourTime
{
    public class DeleteFailureComponentCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
