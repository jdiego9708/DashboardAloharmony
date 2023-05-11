using DasboardAloha.Entities.ModelsBinding;

namespace DashboardAloha.DataAccess.Interfaces
{
    public interface IUsersDesertersDac
    {
        string InsertUserDeserter(UsersDeserterBindingModel user);
    }
}
