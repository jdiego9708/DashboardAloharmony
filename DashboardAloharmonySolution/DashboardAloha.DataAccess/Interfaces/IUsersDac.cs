using DasboardAloha.Entities.ModelsBinding;

namespace DashboardAloha.DataAccess.Interfaces
{
    public interface IUsersDac
    {
        string DeleteAllUsers();
        string InsertUser(UserBindingModelModel user);
    }
}
