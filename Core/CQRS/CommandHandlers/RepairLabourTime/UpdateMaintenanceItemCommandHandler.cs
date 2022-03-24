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
    public class UpdateMaintenanceItemCommandHandler : IRequestHandler<UpdateMaintenanceItemCommand, int>
    {
        private readonly IMaintenanceItemRepository _repository;
        private readonly IMapper _mapper;

        public UpdateMaintenanceItemCommandHandler(IMaintenanceItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateMaintenanceItemCommand request, CancellationToken cancellationToken)
        {
            var maintenanceItem = await _repository.GetMaintenanceItem(request.FailureComponentId, request.Id);
            if (maintenanceItem == null)
            {
                throw new BusinessLogicException("Maintenance item was not found", "Maintenance item was not found");
            }
            _mapper.Map(request, maintenanceItem);
            return await _repository.UpdateMaintenanceItem(maintenanceItem);
        }
    }
}
