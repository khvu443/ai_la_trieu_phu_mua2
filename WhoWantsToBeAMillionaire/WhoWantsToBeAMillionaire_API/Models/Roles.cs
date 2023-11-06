using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WhoWantsToBeAMillionaire_API.Models
{
    public class Roles
    {

        [Key]
        public int role_id { get; set; }
        [Required]
        public string role_name { get; set;}

        [JsonIgnore]
        public Users? User { get; set; }

    }
}
