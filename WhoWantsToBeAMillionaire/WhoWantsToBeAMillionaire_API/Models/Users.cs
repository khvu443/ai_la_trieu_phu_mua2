using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WhoWantsToBeAMillionaire_API.Models
{
    public class Users
    {
        [Key]
        public int user_id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public int role_id { get; set; }

        [JsonIgnore]
        public Scores? Score { get; set; }
        [JsonIgnore]
        public Roles? Role { get; set; }
    }
}
