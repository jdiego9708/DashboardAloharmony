namespace DasboardAloha.Entities.Configuration.ModelsConfiguration
{
    public class ConnectionStringsModel
    {
        public ConnectionStringsModel()
        {
            this.MongoDBDev = string.Empty;
            this.MongoDBProd = string.Empty;
        }
        public string MongoDBDev { get; set; }
        public string MongoDBProd { get; set; }        
    }
}
