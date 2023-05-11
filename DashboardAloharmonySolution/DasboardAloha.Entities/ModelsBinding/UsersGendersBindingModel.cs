namespace DasboardAloha.Entities.ModelsBinding
{
    public class UsersGendersBindingModel
    {
        public UsersGendersBindingModel()
        {
            
        }
        public int Id_users_gender { get; set; }
        public DateTime Date_sync_users { get; set; }
        public TimeSpan Hour_sync_users { get; set; }
        public int Count_users_registers { get; set; }
        public int Count_users_registers_masculine { get; set; }
        public int Count_users_registers_female { get; set; }
        public decimal Percent_users_registers_female { get; set; }
        public decimal Percent_users_registers_masculine { get; set; }

        public int Count_users_actives { get; set; }
        public int Count_users_actives_masculine { get; set; }
        public int Count_users_actives_female { get; set; }
        public decimal Percent_users_actives_masculine { get; set; }
        public decimal Percent_users_actives_female { get; set; }
    }
}
