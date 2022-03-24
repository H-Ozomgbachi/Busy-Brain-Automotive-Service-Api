using AutoMapper;
using Common.Contracts.Exceptions.Types;
using Common.Core.CQRS.Queries.RepairLabourTime;
using Common.Core.Models.RepairLabourTimeModels;
using Common.Data.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Core.CQRS.QueryHandlers.RepairLabourTime
{
    public class GetMaintenanceItemQueryHandler : IRequestHandler<GetMaintenanceItemQuery, MaintenanceItemModel>
    {
        private readonly IMaintenanceItemRepository _repository;
        private readonly IMapper _mapper;

        public GetMaintenanceItemQueryHandler(IMaintenanceItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MaintenanceItemModel> Handle(GetMaintenanceItemQuery request, CancellationToken cancellationToken)
        {
            var maintenanceItem = await _repository.GetMaintenanceItem(request.FailureComponentId, request.Id);
            if (maintenanceItem == null)
            {
                throw new BusinessLogicException($"maintenance item with id : {request.Id} or failure component with id: {request.FailureComponentId} doesn't exist", $"maintenance item with id : {request.Id} or failure component with id: {request.FailureComponentId} doesn't exist");
            }
            return _mapper.Map<MaintenanceItemModel>(maintenanceItem);
        }
    }
}
