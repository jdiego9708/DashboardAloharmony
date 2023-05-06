using DasboardAloha.Entities.Configuration.ModelsConfiguration;
using DasboardAloha.Entities.Helpers;
using DasboardAloha.Entities.Models;
using DasboardAloha.Entities.ModelsBinding;
using DashboardAloha.DataAccess.Interfaces;
using DashboardAloha.Services.Interfaces;
using MongoDB.Bson.IO;

namespace DashboardAloha.Services.Services
{
    public class DashboardService : IDashboardService
    {
        #region CONSTRUCTOR
        public IDashboardDac DashboardDac { get; set; }
        public DashboardService(IDashboardDac DashboardDac)
        {
            this.DashboardDac = DashboardDac;
        }
        #endregion

        #region METHODS
        public async Task<ResponseServiceModel> GetUsersXContentDashboard()
        {
            try
            {
                List<TypeMusicModel> typesMusic = await this.DashboardDac.LoadTypesMusicCollection();

                List<UserModel> users = await this.DashboardDac.GetAllUsersRegisters();

                List<UserModel> usersActive = users.Where(x => x.Total_time_elapsed > 60).ToList();

                int count_users_active = usersActive.Count;

                List<UsersContentBindingModel> usersContents = new();

                foreach(TypeMusicModel type in typesMusic)
                {
                    var typesFind = usersActive.Where(u => u.TypesMusicUserModel.Any(x => x.Type.ToLower() == type.Name.ToLower()));

                    if (typesFind == null)
                        continue;

                    if (typesFind.Count() < 1)
                        continue;

                    int count_users_active_content = typesFind.Count();
                    int count_users_active_free = typesFind.Where(x => x.Membership.Equals("Free")).Count();
                    int count_users_active_courtesy = typesFind.Where(x => x.Membership.Equals("Cortesía")).Count();
                    int count_users_active_premium = typesFind.Where(x => x.Membership.Equals("Premium")).Count();
                    int count_users_active_premium_plus = typesFind.Where(x => x.Membership.Equals("Premium+")).Count();

                    decimal percent_users_active_content = 0;

                    if (count_users_active > 0 && count_users_active_content > 0)
                        percent_users_active_content = ((decimal)count_users_active_content / (decimal)count_users_active) * 100;

                    decimal percent_users_active_free = 0;

                    if (count_users_active > 0 && count_users_active_free > 0)
                        percent_users_active_free = ((decimal)count_users_active_free / (decimal)count_users_active) * 100;

                    decimal percent_users_active_courtesy = 0;

                    if (count_users_active > 0 && count_users_active_courtesy > 0)
                        percent_users_active_courtesy = ((decimal)count_users_active_courtesy / (decimal)count_users_active) * 100;

                    decimal percent_users_active_premium = 0;

                    if (count_users_active > 0 && count_users_active_premium > 0)
                        percent_users_active_premium = ((decimal)count_users_active_premium / (decimal)count_users_active) * 100;

                    decimal percent_users_active_premium_plus = 0;

                    if (count_users_active > 0 && count_users_active_premium_plus > 0)
                        percent_users_active_premium_plus = ((decimal)count_users_active_premium_plus / (decimal)count_users_active) * 100;

                    usersContents.Add(new()
                    {
                        Type_content = type.Name,
                        Total_users_active = count_users_active,
                        Count_users_active_content = count_users_active_content,
                        Count_users_active_free = count_users_active_free,
                        Count_users_active_courtesy = count_users_active_courtesy,
                        Count_users_active_premium = count_users_active_premium,
                        Count_users_active_premium_plus = count_users_active_premium_plus,
                        Percent_users_active_content = percent_users_active_content,
                        Percent_users_active_free = percent_users_active_free,
                        Percent_users_active_courtesy = percent_users_active_courtesy,
                        Percent_users_active_premium = percent_users_active_premium,
                        Percent_users_active_premium_plus = percent_users_active_premium_plus,
                    });
                }
            
                string response = Newtonsoft.Json.JsonConvert.SerializeObject(usersContents);

                return new ResponseServiceModel()
                {
                    IsSuccess = true,
                    Response = response,
                };
            }
            catch (Exception ex)
            {
                return new ResponseServiceModel()
                {
                    IsSuccess = false,
                    Response = ex.Message,
                };
            }
        }
        public async Task<ResponseServiceModel> GetUsersGenderDashboard()
        {
            try
            {
                List<UserModel> users = await this.DashboardDac.GetAllUsersRegisters();

                List<UserModel> usersActive = users.Where(x => x.Total_time_elapsed > 60).ToList();

                int count_users_registers = users.Count;
                int count_users_registers_masculine = users.Where(x => x.Gender.Equals("male")).Count();
                int count_users_registers_female = users.Where(x => x.Gender.Equals("female")).Count();

                decimal percent_users_registers_masculine = 0;

                if (count_users_registers > 0 && count_users_registers_masculine > 0)
                    percent_users_registers_masculine = ((decimal)count_users_registers_masculine / (decimal)count_users_registers) * 100;

                decimal percent_users_registers_female = 0;

                if (count_users_registers > 0 && count_users_registers_female > 0)
                    percent_users_registers_female = ((decimal)count_users_registers_female / (decimal)count_users_registers) * 100;

                int count_users_active = usersActive.Count;
                int count_users_active_masculine = usersActive.Where(x => x.Gender.Equals("male")).Count();
                int count_users_active_female = usersActive.Where(x => x.Gender.Equals("female")).Count();

                decimal percent_users_active_masculine = 0;

                if (count_users_active > 0 && count_users_active_masculine > 0)
                    percent_users_active_masculine = ((decimal)count_users_active_masculine / (decimal)count_users_active) * 100;

                decimal percent_users_active_female = 0;

                if (count_users_active > 0 && count_users_active_female > 0)
                    percent_users_active_female = ((decimal)count_users_active_female / (decimal)count_users_active) * 100;

                UsersGendersBindingModel usersgenderDashboard = new()
                {
                    Count_users_registers = count_users_registers,
                    Count_users_registers_masculine = count_users_registers_masculine,
                    Count_users_registers_female = count_users_registers_female,
                    Percent_users_registers_female = percent_users_registers_female,
                    Percent_users_registers_masculine = percent_users_registers_masculine,
                    Count_users_actives = count_users_active,
                    Count_users_actives_masculine = count_users_active_masculine,
                    Count_users_actives_female = count_users_active_female,
                    Percent_users_actives_masculine = percent_users_active_masculine,
                    Percent_users_actives_female = percent_users_active_female,
                };

                string response = Newtonsoft.Json.JsonConvert.SerializeObject(usersgenderDashboard);

                return new ResponseServiceModel()
                {
                    IsSuccess = true,
                    Response = response,
                };
            }
            catch (Exception ex)
            {
                return new ResponseServiceModel()
                {
                    IsSuccess = false,
                    Response = ex.Message,
                };
            }
        }
        public async Task<ResponseServiceModel> GetUsersActiveDashboard()
        {
            try
            {
                List<UserModel> users = await this.DashboardDac.GetAllUsersRegisters();

                List<UserModel> usersActive = users.Where(x => x.Total_time_elapsed > 60).ToList();

                int count_users_registers = users.Count;
                int count_users_active = usersActive.Count;

                decimal percent_users_active = 0;

                if (count_users_registers > 0 && count_users_active > 0)
                    percent_users_active = ((decimal)count_users_active / (decimal)count_users_registers) * 100;

                int count_users_active_free = usersActive.Where(x => x.Membership.Equals("Free")).Count();
                int count_users_active_courtesy = usersActive.Where(x => x.Membership.Equals("Cortesía")).Count();
                int count_users_active_premium = usersActive.Where(x => x.Membership.Equals("Premium")).Count();
                int count_users_active_premium_plus = usersActive.Where(x => x.Membership.Equals("Premium+")).Count();

                decimal percent_users_active_free = 0;

                if (count_users_active > 0 && count_users_active_free > 0)
                    percent_users_active_free = ((decimal)count_users_active_free / (decimal)count_users_active) * 100;

                decimal percent_users_active_courtesy = 0;

                if (count_users_active > 0 && count_users_active_courtesy > 0)
                    percent_users_active_courtesy = ((decimal)count_users_active_courtesy / (decimal)count_users_active) * 100;
                
                decimal percent_users_active_premium = 0;

                if (count_users_active > 0 && count_users_active_premium > 0)
                    percent_users_active_premium = ((decimal)count_users_active_premium / (decimal)count_users_active) * 100;

                decimal percent_users_active_premium_plus = 0;

                if (count_users_active > 0 && count_users_active_premium_plus > 0)
                    percent_users_active_premium_plus = ((decimal)count_users_active_premium_plus / (decimal)count_users_active) * 100;

                UsersActiveBindingModel usersActiveDashboard = new()
                {
                    Count_users_registers = count_users_registers,
                    Count_users_active = count_users_active,
                    Percent_users_active = percent_users_active,
                    Count_users_active_free = count_users_active_free,
                    Count_users_active_courtesy = count_users_active_courtesy,
                    Count_users_active_premium = count_users_active_premium,
                    Count_users_active_premium_plus = count_users_active_premium_plus,
                    Percent_users_active_free = percent_users_active_free,
                    Percent_users_active_courtesy = percent_users_active_courtesy,
                    Percent_users_active_premium = percent_users_active_premium,
                    Percent_users_active_premium_plus = percent_users_active_premium_plus,
                };

                string response = Newtonsoft.Json.JsonConvert.SerializeObject(usersActiveDashboard);

                return new ResponseServiceModel()
                {
                    IsSuccess = true,
                    Response = response,
                };
            }
            catch (Exception ex)
            {
                return new ResponseServiceModel()
                {
                    IsSuccess = false,
                    Response = ex.Message,
                };
            }
        }
        public async Task<ResponseServiceModel> GetUsersRegisterDashboard()
        {
            try
            {
                List<UserModel> users = await this.DashboardDac.GetAllUsersRegisters();

                int count_users_registers = users.Count;
                int count_users_registers_free = users.Where(x => x.Membership.Equals("Free")).Count();
                int count_users_registers_courtesy = users.Where(x => x.Membership.Equals("Cortesía")).Count();
                int count_users_registers_premium = users.Where(x => x.Membership.Equals("Premium")).Count();
                int count_users_registers_premium_plus = users.Where(x => x.Membership.Equals("Premium+")).Count();
                
                int count_users_registers_active = users.Where(x => x.Total_time_elapsed > 60).Count();

                decimal percent_users_registers_active = 0;

                if (count_users_registers > 0 && count_users_registers_active > 0)
                     percent_users_registers_active = ((decimal)count_users_registers_active / (decimal)count_users_registers) * 100;

                UsersRegistersBindingModel usersRegistersDashboard = new()
                {
                    Count_users_registers = count_users_registers,
                    Count_users_registers_active = count_users_registers_active,
                    Count_users_registers_courtesy = count_users_registers_courtesy,
                    Count_users_registers_free = count_users_registers_free,
                    Count_users_registers_premium = count_users_registers_premium,
                    Count_users_registers_premium_plus = count_users_registers_premium_plus,
                    Percent_users_registers_active = percent_users_registers_active,
                };

                string response = Newtonsoft.Json.JsonConvert.SerializeObject(usersRegistersDashboard);

                RestHelper rest = new();
                var res = rest.CallMethodPostDataBoxAsync("", "");


                return new ResponseServiceModel()
                {
                    IsSuccess = true,
                    Response = response,
                };
            }
            catch (Exception ex)
            {
                return new ResponseServiceModel()
                {
                    IsSuccess = false,
                    Response = ex.Message,
                };
            }
        }
        public async Task<ResponseServiceModel> GetCountUsersActive()
        {
            try
            {
                string users = await this.DashboardDac.GetCountUsersActive();

                return new ResponseServiceModel()
                {
                    IsSuccess = true,
                    Response = users,
                };
            }
            catch (Exception ex)
            {
                return new ResponseServiceModel()
                {
                    IsSuccess = false,
                    Response = ex.Message,
                };
            }
        }
        public async Task<ResponseServiceModel> GetCountUsersRegisters()
        {
            try
            {
                string users = await this.DashboardDac.GetCountUsersRegisters();

                return new ResponseServiceModel()
                {
                    IsSuccess = true,
                    Response = users,
                };
            }
            catch (Exception ex)
            {
                return new ResponseServiceModel()
                {
                    IsSuccess = false,
                    Response = ex.Message,
                };
            }
        }
        public async Task<ResponseServiceModel> GetCountUsersRegistersXMembership()
        {
            try
            {
                string users = await this.DashboardDac.GetCountUsersRegistersXMembership();

                return new ResponseServiceModel()
                {
                    IsSuccess = true,
                    Response = users,
                };
            }
            catch (Exception ex)
            {
                return new ResponseServiceModel()
                {
                    IsSuccess = false,
                    Response = ex.Message,
                };
            }
        }
        #endregion
    }
}
