using DasboardAloha.Entities.ModelsBinding;

namespace DashboardAloha.DataAccess.Interfaces
{
    public interface IUsersGendersDac
    {
        string InsertUserGender(UsersGendersBindingModel user);
    }
}
