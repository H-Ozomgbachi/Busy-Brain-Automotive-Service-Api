using AutoMapper;
using Common.Contracts.Exceptions.Types;
using Common.Core.CQRS.Commands.RepairLabourTime;
using Common.Data.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.CommandHandlers.RepairLabourTime
{
    public class UpdateFailureComponentCommandHandler : IRequestHandler<UpdateFailureComponentCommand, int>
    {
        private readonly IFailureComponentRepository repository;
        private readonly IMapper mapper;

        public UpdateFailureComponentCommandHandler(IFailureComponentRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(UpdateFailureComponentCommand request, CancellationToken cancellationToken)
        {
            var failureComponent = await repository.GetFailureComponent(request.Id);
            if (failureComponent == null)
            {
                throw new BusinessLogicException("Failure component was not found", "Failure component was not found");
            }
            mapper.Map(request, failureComponent);
            return await repository.UpdateFailureComponent(failureComponent);
        }
    }
}
