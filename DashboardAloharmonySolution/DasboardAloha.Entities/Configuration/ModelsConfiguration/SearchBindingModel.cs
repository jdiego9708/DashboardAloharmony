namespace DasboardAloha.Entities.Configuration.ModelsConfiguration
{
    public class SearchBindingModel
    {
        public SearchBindingModel()
        {
            this.Type_search = string.Empty;
            this.Text_search1 = string.Empty;
            this.Text_search2 = string.Empty;
        }
        public string Type_search { get; set; }
        public string Text_search1 { get; set; }
        public string Text_search2 { get; set; }
    }
}
