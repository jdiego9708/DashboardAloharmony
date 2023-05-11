using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DasboardAloha.Entities.Models
{
    public class HistoryPaymentModel
    {
        public HistoryPaymentModel()
        {
            this.CardNumber = string.Empty;
            this.Type = string.Empty;
            this.Description = string.Empty;
            this.Transaction = string.Empty;
        }

        [BsonId]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("idUser")]
        public ObjectId IdUser { get; set; }

        [BsonElement("cardNumber")]
        public string CardNumber { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("amount")]
        public int Amount { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("transaction")]
        public string Transaction { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
