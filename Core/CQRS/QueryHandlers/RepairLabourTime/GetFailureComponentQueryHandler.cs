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
    public class GetFailureComponentQueryHandler : IRequestHandler<GetFailureComponentQuery, FailureComponentModel>
    {
        private readonly IFailureComponentRepository _repository;
        private readonly IMapper _mapper;

        public GetFailureComponentQueryHandler(IFailureComponentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FailureComponentModel> Handle(GetFailureComponentQuery request, CancellationToken cancellationToken)
        {
            var failureComponent = await _repository.GetFailureComponent(request.Id);
            if (failureComponent == null)
            {
                throw new BusinessLogicException($"Failure component with id : {request.Id} doesn't exist", $"Failure component with id : {request.Id} doesn't exist");
            }
            return _mapper.Map<FailureComponentModel>(failureComponent);
        }
    }
}
