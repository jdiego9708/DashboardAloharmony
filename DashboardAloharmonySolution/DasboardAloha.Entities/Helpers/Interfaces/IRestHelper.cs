using DasboardAloha.Entities.Configuration.ModelsConfiguration;

namespace DasboardAloha.Entities.Helpers.Interfaces
{
    public interface IRestHelper
    {
        Task<RestResponseModel> CallMethodPostDataBoxAsync(string data);
    }
}
