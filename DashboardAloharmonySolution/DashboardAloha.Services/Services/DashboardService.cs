using DasboardAloha.Entities.Configuration.ModelsConfiguration;
using DasboardAloha.Entities.Helpers.Interfaces;
using DasboardAloha.Entities.Models;
using DasboardAloha.Entities.ModelsBinding;
using DashboardAloha.DataAccess.Interfaces;
using DashboardAloha.Services.Interfaces;

namespace DashboardAloha.Services.Services
{
    public class DashboardService : IDashboardService
    {
        #region CONSTRUCTOR
        public IUsersDac IUsersDac { get; set; }
        public ITotalSalesDac ITotalSalesDac { get; set; }
        public IUsersRegistersDac IUsersRegistersDac { get; set; }
        public IUsersActivesDac IUsersActivesDac { get; set; }
        public IUsersGendersDac IUsersGendersDac { get; set; }
        public IUsersDesertersDac IUsersDesertersDac { get; set; }
        public IUsersContentDac IUsersContentDac { get; set; }
        public IDashboardDac DashboardDac { get; set; }
        public IRestHelper IRestHelper { get; set; }
        public DashboardService(IDashboardDac DashboardDac,
            IRestHelper IRestHelper,
            IUsersActivesDac iUsersActivesDac,
            IUsersGendersDac iUsersGendersDac,
            IUsersDesertersDac iUsersDesertersDac,
            IUsersContentDac iUsersContentDac,
            IUsersRegistersDac iUsersRegistersDac,
            ITotalSalesDac iTotalSalesDac,
            IUsersDac iUsersDac)
        {
            this.IRestHelper = IRestHelper;
            this.DashboardDac = DashboardDac;
            this.IUsersActivesDac = iUsersActivesDac;
            this.IUsersGendersDac = iUsersGendersDac;
            this.IUsersDesertersDac = iUsersDesertersDac;
            this.IUsersContentDac = iUsersContentDac;
            this.IUsersRegistersDac = iUsersRegistersDac;
            this.ITotalSalesDac = iTotalSalesDac;
            this.IUsersDac = iUsersDac;
        }
        #endregion

        #region METHODS
        public async Task<ResponseServiceModel> GetTotalSalesDashboard()
        {
            try
            {
                List<HistoryPaymentModel> payments = await this.DashboardDac.LoadHistoryPaymentCollection();

                List<UserModel> users = await this.DashboardDac.GetAllUsersRegisters();

                List<UserModel> usersActive = users.Where(x => x.Total_time_elapsed > 60).ToList();

                List<UserModel> usersPayments = users.Where(x => x.Membership.ToLower() != "free" && x.Membership.ToLower() != "cortesía").ToList();

                if (usersPayments.Count < 1)
                    throw new Exception("No hay pagos de ningun usuario");

                int count_users_active = usersActive.Count;

                int count_users_payments = usersPayments.Count;

                List<UserModel> usersSales = new();

                foreach(HistoryPaymentModel payment in payments)
                {
                    var usersFind = users.Where(u => u.Id.ToString() == payment.IdUser.ToString());

                    if (usersFind == null)
                        continue;

                    if (usersFind.Count() < 1)
                        continue;

                    foreach (UserModel us in usersFind)
                    {
                        us.Payments = new();

                        var paymentsUser = payments.Where(x => x.IdUser.ToString() == us.Id.ToString());

                        us.Payments.AddRange(paymentsUser);

                        usersSales.Add(us); 
                    }
                }

                //foreach (UserModel userActive in usersPayments)
                //{
                //    var paymentsFind = payments.Where(u => u.IdUser == userActive.Id);

                //    if (paymentsFind == null)
                //        continue;

                //    if (paymentsFind.Count() < 1)
                //        continue;

                //    userActive.Payments = new();
                //    userActive.Payments.AddRange(paymentsFind);
                //}

                int count_users_sales = usersSales.Count;
                int count_users_sales_premium = usersSales.Where(x => x.Membership.Equals("Premium")).Count();
                int count_users_sales_premium_plus = usersSales.Where(x => x.Membership.Equals("Premium+")).Count();

                decimal percent_users_sales_premium = 0;

                if (count_users_sales > 0 && count_users_sales_premium > 0)
                    percent_users_sales_premium = ((decimal)count_users_sales_premium / (decimal)count_users_sales) * 100;

                decimal percent_users_sales_premium_plus = 0;

                if (count_users_sales > 0 && count_users_sales_premium_plus > 0)
                    percent_users_sales_premium_plus = ((decimal)count_users_sales_premium_plus / (decimal)count_users_sales) * 100;

                TotalSalesBindingModel totalSale = new()
                {
                    Date_sync_users = DateTime.Now,
                    Hour_sync_users = DateTime.Now.TimeOfDay,
                    Count_users_active = count_users_active,
                    Count_users_payments = count_users_payments,
                    Count_users_sales = count_users_sales,
                    Count_users_sales_premium = count_users_sales_premium,
                    Count_users_sales_premium_plus = count_users_sales_premium_plus,
                    Percent_users_sales_premium = percent_users_sales_premium,
                    Percent_users_sales_premium_plus = percent_users_sales_premium_plus,
                };

                await Task.Run(() => this.ITotalSalesDac.InsertTotalSale(totalSale));

                string response = Newtonsoft.Json.JsonConvert.SerializeObject(totalSale);

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
        public async Task<ResponseServiceModel> GetUsersDesertersDashboard()
        {
            try
            {
                List<HistoryPaymentModel> payments = await this.DashboardDac.LoadHistoryPaymentCollection();

                List<UserModel> users = await this.DashboardDac.GetAllUsersRegisters();

                List<UserModel> usersActive = users.Where(x => x.Total_time_elapsed > 60).ToList();

                List<UserModel> usersPayments = users.Where(x => x.Membership.ToLower() != "free" && x.Membership.ToLower() != "cortesía").ToList();

                if (usersPayments.Count < 1)
                    throw new Exception("No hay pagos de ningun usuario");

                int count_users_active = usersActive.Count;

                int count_users_payments = usersPayments.Count;

                List<UserModel> usersDeserters = new();

                foreach (UserModel userActive in usersPayments)
                {
                    var paymentsFind = payments.Where(u => u.IdUser == userActive.Id);

                    if (paymentsFind == null)
                        continue;

                    if (paymentsFind.Count() < 1)
                        continue;

                    userActive.Payments = new();
                    userActive.Payments.AddRange(paymentsFind);

                    var lastPayment = paymentsFind.OrderByDescending(u => u.CreatedAt).FirstOrDefault();

                    if (lastPayment == null)
                        continue;

                    if (lastPayment.Description.Equals("Buy Membership premiumPlusMonthly"))
                    {
                        DateTime dateEndPayment = lastPayment.CreatedAt.AddMonths(1);

                        if (DateTime.Now > dateEndPayment)
                        {
                            userActive.IsDeserted = true;
                            usersDeserters.Add(userActive);
                        }
                    }
                    else if (lastPayment.Description.Equals("Buy Membership premiumPlusYearly"))
                    {
                        DateTime dateEndPayment = lastPayment.CreatedAt.AddYears(1);

                        if (DateTime.Now > dateEndPayment)
                        {
                            userActive.IsDeserted = true;
                            usersDeserters.Add(userActive);
                        }
                    }
                    else
                    {
                        userActive.IsDeserted = true;
                        usersDeserters.Add(userActive);
                    }
                }

                int count_users_deserters = usersDeserters.Count;
                int count_users_deserter_premium = usersDeserters.Where(x => x.Membership.Equals("Premium")).Count();
                int count_users_deserter_premium_plus = usersDeserters.Where(x => x.Membership.Equals("Premium+")).Count();

                decimal percent_users_deserter_premium = 0;

                if (count_users_deserters > 0 && count_users_deserter_premium > 0)
                    percent_users_deserter_premium = ((decimal)count_users_deserter_premium / (decimal)count_users_deserters) * 100;

                decimal percent_users_deserter_premium_plus = 0;

                if (count_users_deserters > 0 && count_users_deserter_premium_plus > 0)
                    percent_users_deserter_premium_plus = ((decimal)count_users_deserter_premium_plus / (decimal)count_users_deserters) * 100;

                UsersDeserterBindingModel userDeserter = new()
                {
                    Date_sync_users = DateTime.Now,
                    Hour_sync_users = DateTime.Now.TimeOfDay,
                    Count_users_active = count_users_active,
                    Count_users_deserter = count_users_deserters,
                    Count_users_deserter_premium = count_users_deserter_premium,
                    Count_users_deserter_premium_plus = count_users_deserter_premium_plus,
                    Percent_users_deserter_premium = percent_users_deserter_premium,
                    Percent_users_deserter_premium_plus = percent_users_deserter_premium_plus,
                };

                await Task.Run(() => this.IUsersDesertersDac.InsertUserDeserter(userDeserter));

                string response = Newtonsoft.Json.JsonConvert.SerializeObject(userDeserter);

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
        public async Task<ResponseServiceModel> GetUsersXContentDashboard()
        {
            try
            {
                List<TypeMusicModel> typesMusic = await this.DashboardDac.LoadTypesMusicCollection();

                List<UserModel> users = await this.DashboardDac.GetAllUsersRegisters();

                List<UserModel> usersActive = users.Where(x => x.Total_time_elapsed > 60).ToList();

                int count_users_active = usersActive.Count;

                List<UsersContentBindingModel> usersContents = new();

                foreach (TypeMusicModel type in typesMusic)
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
                        Date_sync_users = DateTime.Now,
                        Hour_sync_users = DateTime.Now.TimeOfDay,
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

                await Task.Run(() => usersContents.ForEach(x => this.IUsersContentDac.InsertUserContent(x)));

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
                    Date_sync_users = DateTime.Now,
                    Hour_sync_users = DateTime.Now.TimeOfDay,
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

                await Task.Run(() => this.IUsersGendersDac.InsertUserGender(usersgenderDashboard));

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
                    Date_sync_users = DateTime.Now,
                    Hour_sync_users = DateTime.Now.TimeOfDay,
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

                await Task.Run(() => this.IUsersActivesDac.InsertUserActives(usersActiveDashboard));

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
                    Date_sync_users = DateTime.Now,
                    Hour_sync_users = DateTime.Now.TimeOfDay,
                    Count_users_registers = count_users_registers,
                    Count_users_registers_active = count_users_registers_active,
                    Count_users_registers_courtesy = count_users_registers_courtesy,
                    Count_users_registers_free = count_users_registers_free,
                    Count_users_registers_premium = count_users_registers_premium,
                    Count_users_registers_premium_plus = count_users_registers_premium_plus,
                    Percent_users_registers_active = percent_users_registers_active,
                };

                await Task.Run(() => this.IUsersRegistersDac.InsertUserRegister(usersRegistersDashboard));

                string response = Newtonsoft.Json.JsonConvert.SerializeObject(usersRegistersDashboard);

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
        public async Task<ResponseServiceModel> GetListUsersActives()
        {
            try
            {
                List<UserModel> users = await this.DashboardDac.GetAllUsersRegisters();

                List<UserModel> usersActive = users.Where(x => x.Total_time_elapsed > 60).ToList();

                this.IUsersDac.DeleteAllUsers();

                await Task.Run(() => usersActive.ForEach(x => this.IUsersDac.InsertUser(new(x))));

                return new ResponseServiceModel()
                {
                    IsSuccess = true,
                    Response = Newtonsoft.Json.JsonConvert.SerializeObject(usersActive),
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
