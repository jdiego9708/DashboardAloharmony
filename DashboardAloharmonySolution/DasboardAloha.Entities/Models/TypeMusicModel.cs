using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DasboardAloha.Entities.Models
{
    public class TypeMusicModel
    {
        public TypeMusicModel()
        {
            this.Type = string.Empty;
            this.Name = string.Empty;
        }
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("language")]
        public ObjectId Language { get; set; }
        [BsonElement("order")]
        public int Order { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
    }
}
