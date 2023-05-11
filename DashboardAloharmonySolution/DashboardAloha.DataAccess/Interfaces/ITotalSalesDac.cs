using DasboardAloha.Entities.ModelsBinding;

namespace DashboardAloha.DataAccess.Interfaces
{
    public interface ITotalSalesDac
    {
        string InsertTotalSale(TotalSalesBindingModel total);
    }
}
