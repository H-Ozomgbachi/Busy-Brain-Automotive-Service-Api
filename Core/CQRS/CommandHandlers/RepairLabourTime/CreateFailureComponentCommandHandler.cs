using Common.Core.CQRS.Commands.RepairLabourTime;
using Common.Data.Domain.RepairLabourTime;
using Common.Data.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.CommandHandlers.RepairLabourTime
{
    public class CreateFailureComponentCommandHandler : IRequestHandler<CreateFailureComponentCommand, int>
    {
        private readonly IFailureComponentRepository repository;

        public CreateFailureComponentCommandHandler(IFailureComponentRepository repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(CreateFailureComponentCommand request, CancellationToken cancellationToken)
        {
            var failureComponent = new FailureComponent(request.Title, request.AssemblyOrSystemName, request.CreatedAt, request.ModifiedBy);
            return await repository.CreateFailureComponent(failureComponent);
        }
    }
}
