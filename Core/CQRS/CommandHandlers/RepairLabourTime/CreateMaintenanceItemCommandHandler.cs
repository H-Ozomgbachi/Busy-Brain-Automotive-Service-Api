using Common.Core.CQRS.Commands.RepairLabourTime;
using Common.Data.Domain.RepairLabourTime;
using Common.Data.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.CommandHandlers.RepairLabourTime
{
    public class CreateMaintenanceItemCommandHandler : IRequestHandler<CreateMaintenanceItemCommand, int>
    {
        private readonly IMaintenanceItemRepository _repository;

        public CreateMaintenanceItemCommandHandler(IMaintenanceItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateMaintenanceItemCommand request, CancellationToken cancellationToken)
        {
            var maintenanceItem = new MaintenanceItem(request.Title, request.Code, request.LabourTimeHours, request.TruckModel, request.CostPerHour, request.FailureComponentId, request.CreatedAt, DateTime.Now, request.ModifiedBy);
            return await _repository.CreateMaintenanceItem(maintenanceItem);
        }
    }
}
