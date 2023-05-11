using DasboardAloha.Entities.ModelsBinding;

namespace DashboardAloha.DataAccess.Interfaces
{
    public interface IUsersContentDac
    {
        string InsertUserContent(UsersContentBindingModel user);
    }
}
