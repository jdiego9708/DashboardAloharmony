namespace DasboardAloha.Entities.ModelsBinding
{
    public class UsersDeserterBindingModel
    {
        public UsersDeserterBindingModel()
        {
            
        }
        public int Id_users_deserter { get; set; }
        public DateTime Date_sync_users { get; set; }
        public TimeSpan Hour_sync_users { get; set; }
        public int Count_users_active { get; set; }
        public int Count_users_payment { get; set; }
        public int Count_users_deserter { get; set; }
        public int Count_users_deserter_premium { get; set; }
        public int Count_users_deserter_premium_plus { get; set; }
        public decimal Percent_users_deserter_premium { get; set; }
        public decimal Percent_users_deserter_premium_plus { get; set; }
    }
}