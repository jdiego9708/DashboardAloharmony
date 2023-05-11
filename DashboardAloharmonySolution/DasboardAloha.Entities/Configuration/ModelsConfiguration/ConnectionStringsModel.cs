namespace DasboardAloha.Entities.Configuration.ModelsConfiguration
{
    public class ConnectionStringsModel
    {
        public ConnectionStringsModel()
        {
            this.MongoDBDev = string.Empty;
            this.MongoDBProd = string.Empty;
            this.SQLAlohaDashboardDBDev = string.Empty;
            this.SQLAlohaDashboardDBProd = string.Empty;
        }
        public string MongoDBDev { get; set; }
        public string MongoDBProd { get; set; }        
        public string SQLAlohaDashboardDBDev { get; set; }        
        public string SQLAlohaDashboardDBProd { get; set; }
    }
}
