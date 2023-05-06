namespace DasboardAloha.Entities.Configuration.ModelsConfiguration
{
    public class ConfigurationDashboardAPI
    {
        public ConfigurationDashboardAPI()
        {
            this.NameBDDefault = string.Empty;
        }
        public bool IsProduction { get; set; }
        public string NameBDDefault{ get; set; }
    }
}
