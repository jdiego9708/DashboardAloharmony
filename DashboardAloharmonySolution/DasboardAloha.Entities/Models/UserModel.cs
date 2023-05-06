using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using DasboardAloha.Entities.ModelsBinding;

namespace DasboardAloha.Entities.Models
{
    public class UserModel
    {
        public UserModel()
        {
            

            this.Email = string.Empty;
            this.Phone = string.Empty;
            this.Device = string.Empty;
            this.FullName = string.Empty;
            this.Pin = string.Empty;
            this.Profile = string.Empty;
            this.Gender = string.Empty;
            this.Membership = string.Empty;
            this.ExpoToken = string.Empty;
            this.Country = string.Empty;
            this.UsedReferralCode = string.Empty;
            this.DateBirth = string.Empty;
            this.Adv = string.Empty;
            this.TimeZone = string.Empty;
            this.TypeMembership = string.Empty;
            this.ExpDateMembership = string.Empty;
            this.StatusMembership = string.Empty;
            this.UserReferralCode = string.Empty;


            this.HistoryTracks = new();
            this.Modules = new();

            this.ReferralCode = string.Empty;
            this.Password = string.Empty;   
            this.Shortlink = string.Empty;
            this.Stats = string.Empty;
            this.WakeUp = string.Empty;
            this.Plan = string.Empty;
            this.IdClientify = string.Empty;
            this.Cortesia = string.Empty;
            this.Register = string.Empty;
            this.CancelMembership = string.Empty;
            this.MembershipCost = string.Empty;
            this.DialCode = string.Empty;
            this.LastName = string.Empty;
        }

        public List<TypeMusicUserModel> TypesMusicUserModel { get; set; }
        public decimal Total_time_elapsed { get; set; }
        public List<HistoryTrackModel> HistoryTracks { get; set; }

        [BsonId]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

        [BsonElement("device")]
        public string Device { get; set; }

        [BsonElement("active")]
        public bool Active { get; set; }

        [BsonElement("fullRecord")]
        public bool FullRecord { get; set; }

        [BsonElement("fullName")]
        public string FullName { get; set; }

        [BsonElement("pin")]
        public string Pin { get; set; }

        [BsonElement("profile")]
        public string Profile { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; }

        [BsonElement("referralCode")]
        public object ReferralCode { get; set; }

        [BsonElement("membership")]
        public string Membership { get; set; }

        [BsonElement("expoToken")]
        public string ExpoToken { get; set; }

        [BsonElement("country")]
        public string Country { get; set; }

        [BsonElement("usedReferralCode")]
        public string UsedReferralCode { get; set; }

        [BsonElement("userReferralCode")]
        public string UserReferralCode { get; set; }

        [BsonElement("dateBirth")]
        public string DateBirth { get; set; }

        [BsonElement("language")]
        public ObjectId Language { get; set; }

        [BsonElement("adv")]
        public string Adv { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [BsonElement("timeZone")]
        public string TimeZone { get; set; }

        [BsonElement("TypeMembership")]
        public string TypeMembership { get; set; }

        [BsonElement("expDateMembership")]
        public object ExpDateMembership { get; set; }

        [BsonElement("statusMembership")]
        public string StatusMembership { get; set; }

        [BsonElement("disabled")]
        public bool Disabled { get; set; }

        [BsonElement("Premium")]
        public int Premium { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("modules")]
        public List<string> Modules { get; set; }

        [BsonElement("shortlink")]
        public string Shortlink { get; set; }

        [BsonElement("lastLogin")]
        public DateTime LastLogin { get; set; }

        [BsonElement("stats")]
        public object Stats { get; set; }

        [BsonElement("wakeUp")]
        public object WakeUp { get; set; }

        [BsonElement("plan")]
        public object Plan { get; set; }

        [BsonElement("idClientify")]
        public object IdClientify { get; set; }

        [BsonElement("cortesia")]
        public object Cortesia { get; set; }

        [BsonElement("Register")]
        public object Register { get; set; }

        [BsonElement("cancelMembership")]
        public object CancelMembership { get; set; }

        [BsonElement("membershipCost")]
        public object MembershipCost { get; set; }

        [BsonElement("dialCode")]
        public object DialCode { get; set; }

        [BsonElement("lastName")]
        public object LastName { get; set; }
    }
}
