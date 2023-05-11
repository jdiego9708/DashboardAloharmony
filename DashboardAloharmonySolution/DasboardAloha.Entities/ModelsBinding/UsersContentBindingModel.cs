namespace DasboardAloha.Entities.ModelsBinding
{
    public class UsersContentBindingModel
    {
        public UsersContentBindingModel()
        {
            this.Type_content = string.Empty;
        }
        public int Id_users_content { get; set; }
        public DateTime Date_sync_users { get; set; }
        public TimeSpan Hour_sync_users { get; set; }
        public string Type_content { get; set; }
        public int Total_users_active { get; set; }
        public int Count_users_active_content { get; set; }
        public int Count_users_active_free { get; set; }
        public int Count_users_active_courtesy { get; set; }
        public int Count_users_active_premium { get; set; }
        public int Count_users_active_premium_plus { get; set; }
        public decimal Percent_users_active_content { get; set; }
        public decimal Percent_users_active_free { get; set; }
        public decimal Percent_users_active_courtesy { get; set; }
        public decimal Percent_users_active_premium { get; set; }
        public decimal Percent_users_active_premium_plus { get; set; }
    }
}
