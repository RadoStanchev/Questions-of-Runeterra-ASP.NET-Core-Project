using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace QuestionsOfRuneterra.Data.Models
{
    public class QuizGame
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public IEnumerable<QuizGameSession> Sessions { get; set; }

        public DateTime StartedOn { get; set; }

        public DateTime FinishedOn { get; set; }

        public string PlayerId { get; set; }

        public IdentityUser Player { get; set; }
    }
}
