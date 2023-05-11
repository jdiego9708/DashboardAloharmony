namespace DasboardAloha.Entities.Configuration.ModelsConfiguration
{
    public class ConfigurationDataBox
    {
        public ConfigurationDataBox()
        {
            this.TokenAccess = string.Empty;
            this.ApiURLProd = string.Empty;
            this.ApiURLDev = string.Empty;
        }
        public string TokenAccess { get; set; }
        public string ApiURLProd { get; set; }
        public string ApiURLDev { get; set; }
    }
}
