﻿using DasboardAloha.Entities.ModelsBinding;

namespace DashboardAloha.DataAccess.Interfaces
{
    public interface IUsersRegistersDac
    {
        string InsertUserRegister(UsersRegistersBindingModel user);
    }
}
