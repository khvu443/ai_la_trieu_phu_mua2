using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WhoWantsToBeAMillionaire_API.Models
{
    public class Scores
    {
        [Key]
        public int score_id { get; set; }
        [Required]
        public int user_id { get; set; }
        [Required]
        public int score { get; set; }

        [JsonIgnore]
        public ICollection<Users>? users { get; set; }

    }
}
