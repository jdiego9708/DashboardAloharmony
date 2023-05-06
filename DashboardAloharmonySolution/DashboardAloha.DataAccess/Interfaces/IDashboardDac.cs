using DasboardAloha.Entities.Models;

namespace DashboardAloha.DataAccess.Interfaces
{
    public interface IDashboardDac
    {
        Task<List<TypeMusicModel>> LoadTypesMusicCollection();
        Task<string> GetCountUsersRegistersXMembership();
        Task<string> GetCountUsersRegisters();
        Task<string> GetCountUsersActive();
        Task<List<UserModel>> GetAllUsersRegisters();
        Task<List<UserModel>> LoadUsersCollection();
        Task<List<HistoryTrackModel>> LoadHistoryTracksCollection();
        Task<List<HistoryViewModel>> LoadHistoryViewsCollection();
    }
}
