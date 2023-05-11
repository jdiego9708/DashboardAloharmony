using DasboardAloha.Entities.Models;

namespace DasboardAloha.Entities.ModelsBinding
{
    public class UserBindingModelModel
    {
        public UserBindingModelModel()
        {
            this.Email_user = string.Empty;
            this.Phone = string.Empty;
            this.Device = string.Empty;
            this.FullName = string.Empty;
            this.Gender = string.Empty;
            this.Membership = string.Empty;
            this.LastName = string.Empty;
            this.Profile = string.Empty;
            this.Country = string.Empty;
            this.Status_membership = string.Empty;
        }
        public UserBindingModelModel(UserModel user)
        {
            this.Total_time_elapsed = user.Total_time_elapsed;
            this.Date_sync_users = DateTime.Now;
            this.Hour_sync_users = DateTime.Now.TimeOfDay;
            this.Email_user = user.Email;
            this.Phone = user.Phone;
            this.Device = user.Device;
            this.FullName = user.FullName;
            this.Gender = user.Gender;
            this.Membership = user.Membership;
            this.LastName = user.LastName;
            this.Created_user = user.CreatedAt;
            this.Profile = user.Profile;
            this.Country = user.Country;
            this.Status_membership = user.StatusMembership;
        }

        public decimal Total_time_elapsed { get; set; }
        public bool IsDeserted { get; set; }
        public int Id_user { get; set; }
        public DateTime Date_sync_users { get; set; }
        public TimeSpan Hour_sync_users { get; set; }
        public string Email_user { get; set; }
        public string Phone { get; set; }
        public string Device { get; set; }
        public string FullName { get; set; }
        public object LastName { get; set; }
        public string Gender { get; set; }
        public string Membership { get; set; }    
        public string Profile { get; set; }
        public string Country { get; set; }
        public string Status_membership { get; set; }
        public DateTime Created_user { get; set; }
    }
}
