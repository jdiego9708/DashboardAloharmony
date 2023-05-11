using DasboardAloha.Entities.ModelsBinding;

namespace DashboardAloha.DataAccess.Interfaces
{
    public interface IUsersActivesDac
    {
        string InsertUserActives(UsersActiveBindingModel user);
    }
}
