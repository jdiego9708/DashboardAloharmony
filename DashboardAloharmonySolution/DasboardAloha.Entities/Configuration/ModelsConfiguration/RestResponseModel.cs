namespace DasboardAloha.Entities.Configuration.ModelsConfiguration
{
    public class RestResponseModel
    {
        public RestResponseModel()
        {
            this.Message = string.Empty;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
