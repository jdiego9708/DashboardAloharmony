using DasboardAloha.Entities.Configuration.ModelsConfiguration;
using DashboardAloha.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DashboardAloha.DataAccess.Dacs
{
    public class ConnectionDac : IConnectionDac
    {
        private readonly ConfigurationDashboardAPI ConfigurationDashboardAPI;
        private readonly ConnectionStringsModel ConnectionStrings;
        public ConnectionDac(IConfiguration IConfiguration)
        {
            var settings = IConfiguration.GetSection("ConfigurationDashboardAPI");
            this.ConfigurationDashboardAPI = settings.Get<ConfigurationDashboardAPI>();

            settings = IConfiguration.GetSection("ConnectionStrings");
            this.ConnectionStrings = settings.Get<ConnectionStringsModel>();
        }

        public string CnMongo()
        {
            if (this.ConfigurationDashboardAPI.IsProduction)
            {
                return this.ConnectionStrings.MongoDBProd;
            }
            else
            {
                return this.ConnectionStrings.MongoDBDev;
            }
        }
        public string CnSQL()
        {
            if (this.ConfigurationDashboardAPI.IsProduction)
            {
                return this.ConnectionStrings.SQLAlohaDashboardDBProd;
            }
            else
            {
                return this.ConnectionStrings.SQLAlohaDashboardDBDev;
            }
        }
    }
}
