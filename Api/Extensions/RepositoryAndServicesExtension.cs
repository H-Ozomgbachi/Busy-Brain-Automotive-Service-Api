using Common.Core.Services;
using Common.Core.Services.PDFService;
using Common.Core.Services.RepairLabourTimeService;
using Common.Core.Services.StandardRepairFeature;
using Common.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.Extensions
{
    public static class RepositoryAndServicesExtension
    {
        public static IServiceCollection AddRepositoriesAndServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IOrganisationRepository, OrganisationRepository>();
            services.AddTransient<IOrganisationService, OrganisationService>();
            services.AddTransient<IUtilityService, UtilityService>();

            services.AddTransient<IFailureComponentRepository, FailureComponentRepository>();
            services.AddTransient<IFailureComponentService, FailureComponentService>();
            services.AddTransient<IMaintenanceItemRepository, MaintenanceItemRepository>();
            services.AddTransient<IMaintenanceItemService, MaintenanceItemService>();
            services.AddTransient<IStandardRepairService, StandardRepairService>();

            services.AddScoped<ICustomPDFGenerator, CustomPDFGenerator>();

            return services;
        }
    }
}
