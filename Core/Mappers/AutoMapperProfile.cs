using AutoMapper;
using Common.Contracts.v1.Account;
using Common.Core.CQRS.Commands.RepairLabourTime;
using Common.Core.Models;
using Common.Core.Models.RepairLabourTimeModels;
using Common.Data.Domain;
using Common.Data.Domain.RepairLabourTime;

namespace Common.Core.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<RegisterUserModel, User>();
            CreateMap<UpdateUserModel, User>().ReverseMap();
            CreateMap<Organisation, OrganisationViewModel>().ReverseMap();

            CreateMap<FailureComponent, FailureComponentModel>().ReverseMap();
            CreateMap<FailureComponent, UpdateFailureComponentCommand>().ReverseMap();
            CreateMap<MaintenanceItem, MaintenanceItemModel>().ReverseMap();
            CreateMap<MaintenanceItem, UpdateMaintenanceItemCommand>().ReverseMap();
        }
    }
}
