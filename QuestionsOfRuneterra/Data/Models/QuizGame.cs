using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuestionsOfRuneterra.Data.Models
{
    public class QuizGame
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public IEnumerable<QuizGameSession> Sessions { get; set; }

        [Required]
        public DateTime StartedOn { get; set; }

        public DateTime FinishedOn { get; set; }

        [Required]
        public string PlayerId { get; set; }

        [Required]
        public ApplicationUser Player { get; set; }
    }
}
