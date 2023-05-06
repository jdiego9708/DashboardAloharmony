namespace DasboardAloha.Entities.ModelsBinding
{
    public class UsersDeserterBindingModel
    {
        public UsersDeserterBindingModel()
        {
            
        }

        public int Count_users_active { get; set; }
        public int Count_users_deserter { get; set; }
        public int Count_users_deserter_free { get; set; }
        public int Count_users_deserter_courtesy { get; set; }
        public int Count_users_deserter_premium { get; set; }
        public int Count_users_deserter_premium_plus { get; set; }
        public decimal Percent_users_deserter_free { get; set; }
        public decimal Percent_users_deserter_courtesy { get; set; }
        public decimal Percent_users_deserter_premium { get; set; }
        public decimal Percent_users_deserter_premium_plus { get; set; }
    }
}