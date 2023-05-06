namespace DasboardAloha.Entities.ModelsBinding
{
    public class UsersActiveBindingModel
    {
        public int Count_users_registers { get; set; }
        public int Count_users_active { get; set; } 
        public decimal Percent_users_active { get; set; }

        public int Count_users_active_free { get; set; }
        public int Count_users_active_courtesy { get; set; }
        public int Count_users_active_premium { get; set; }
        public int Count_users_active_premium_plus { get; set; }
        public decimal Percent_users_active_free { get; set; }
        public decimal Percent_users_active_courtesy { get; set; }
        public decimal Percent_users_active_premium { get; set; }
        public decimal Percent_users_active_premium_plus { get; set; }
    }
}
