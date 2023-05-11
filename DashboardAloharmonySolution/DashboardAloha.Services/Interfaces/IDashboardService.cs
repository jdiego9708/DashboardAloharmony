using DasboardAloha.Entities.Configuration.ModelsConfiguration;

namespace DashboardAloha.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<ResponseServiceModel> GetTotalSalesDashboard();
        Task<ResponseServiceModel> GetUsersDesertersDashboard();
        Task<ResponseServiceModel> GetListUsersActives();
        Task<ResponseServiceModel> GetUsersXContentDashboard();
        Task<ResponseServiceModel> GetUsersGenderDashboard();
        Task<ResponseServiceModel> GetUsersActiveDashboard();
        Task<ResponseServiceModel> GetUsersRegisterDashboard();
        Task<ResponseServiceModel> GetCountUsersActive();
        Task<ResponseServiceModel> GetCountUsersRegistersXMembership();
        Task<ResponseServiceModel> GetCountUsersRegisters();
    }
}
