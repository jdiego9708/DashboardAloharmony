using MongoDB.Bson;

namespace DasboardAloha.Entities.ModelsBinding
{
    public class TypeMusicUserModel
    {
        public TypeMusicUserModel()
        {
            this.Type = string.Empty;
        }
        public ObjectId Id { get; set; }
        public string Type { get; set; }
    }
}
