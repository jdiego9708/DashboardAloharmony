using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DasboardAloha.Entities.Models
{
    public class HistoryViewModel
    {
        public HistoryViewModel()
        {
            this.Type = string.Empty;
            this.Membership = string.Empty;
            this.BrainWaves = string.Empty;
            this.MusicType = string.Empty;
        }

        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("idTrack")]
        public ObjectId IdTrack { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("membership")]
        public string Membership { get; set; }

        [BsonElement("track_time")]
        public int TrackTime { get; set; }

        [BsonElement("brainWaves")]
        public string BrainWaves { get; set; }

        [BsonElement("musicType")]
        public string MusicType { get; set; }

        [BsonElement("time_elapsed")]
        public int TimeElapsed { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
