namespace DasboardAloha.Entities.ModelsBinding
{
    public class TotalSalesBindingModel
    {
        public int Id_total_sale { get; set; }
        public DateTime Date_sync_users { get; set; }
        public TimeSpan Hour_sync_users { get; set; }
        public int Count_users_active { get; set; }
        public int Count_users_payments { get; set; }
        public int Count_users_sales { get; set; }
        public int Count_users_sales_premium { get; set; }
        public int Count_users_sales_premium_plus { get; set; }
        public decimal Percent_users_sales_premium { get; set; }
        public decimal Percent_users_sales_premium_plus { get; set; }
    }
}
