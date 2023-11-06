using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WhoWantsToBeAMillionaire_API.Models
{
    [PrimaryKey(nameof(answer_id), nameof(question_id))]
    public class Answers
    {
        [Required]
        public string answer_id { get; set; }
        [Required]
        public int question_id { get; set; }
        [Required]
        public string answer_content { get; set;}
        [Required]
        public bool isCorrect { get; set; }

        [JsonIgnore]
        public Questions? Question { get; set; }
    }
}
