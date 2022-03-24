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
    public class GetMaintenanceItemByCodeQueryHandler : IRequestHandler<GetMaintenanceItemByCodeQuery, MaintenanceItemModel>
    {
        private readonly IMaintenanceItemRepository _repository;
        private readonly IMapper _mapper;

        public GetMaintenanceItemByCodeQueryHandler(IMaintenanceItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MaintenanceItemModel> Handle(GetMaintenanceItemByCodeQuery request, CancellationToken cancellationToken)
        {
            var maintenanceItem = await _repository.GetMaintenanceItemByCode(request.Code);
            if (maintenanceItem == null)
            {
                throw new BusinessLogicException($"maintenance item with code : {request.Code} doesn't exist", $"maintenance item with code : {request.Code} doesn't exist");
            }
            return _mapper.Map<MaintenanceItemModel>(maintenanceItem);
        }
    }
}
