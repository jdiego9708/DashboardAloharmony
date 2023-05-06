using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using DasboardAloha.Entities.ModelsBinding;

namespace DasboardAloha.Entities.Models
{
    public class HistoryTrackModel
    {
        public HistoryTrackModel()
        {
            this.HistoryViews = new();
            this.TypesMusicUserModel = new();
        }

        public List<TypeMusicUserModel> TypesMusicUserModel { get; set; }
        public decimal Total_time_elapsed { get; set; }
        public List<HistoryViewModel> HistoryViews { get; set; }

        [BsonId]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("idTrack")]
        public ObjectId IdTrack { get; set; }

        [BsonElement("idUser")]
        public ObjectId IdUser { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
