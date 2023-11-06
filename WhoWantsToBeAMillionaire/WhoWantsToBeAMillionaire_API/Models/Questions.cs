using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WhoWantsToBeAMillionaire_API.Models
{
    public class Questions
    {
        [Key]
        public int question_id { get; set; }
        [Required]
        public string question_content { get; set; }

        [JsonIgnore]
        public ICollection<Answers>? answers { get; set; }
    }
}
