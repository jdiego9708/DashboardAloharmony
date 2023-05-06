namespace DasboardAloha.Entities.Configuration.ModelsConfiguration
{
    public class ResponseServiceModel
    {
        public ResponseServiceModel()
        {
            this.Response = string.Empty;
        }
        public bool IsSuccess { get; set; }
        public string Response { get; set; }
    }
}
