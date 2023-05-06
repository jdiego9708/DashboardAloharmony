namespace DasboardAloha.Entities.ModelsBinding
{
    public class UsersRegistersBindingModel
    {
        public UsersRegistersBindingModel()
        {
            
        }
        public int Count_users_registers { get; set; }
        public int Count_users_registers_free { get; set; }
        public int Count_users_registers_courtesy { get; set; }
        public int Count_users_registers_premium { get; set; }
        public int Count_users_registers_premium_plus { get; set; }
        public int Count_users_registers_active { get; set; }
        public decimal Percent_users_registers_active { get; set; }
    }
}
