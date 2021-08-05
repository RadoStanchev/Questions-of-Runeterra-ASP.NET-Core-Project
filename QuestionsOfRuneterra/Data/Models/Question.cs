using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace QuestionsOfRuneterra.Data.Models
{
    public class Question
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Content { get; set; }

        public Answer RightAnswer { get; set; }

        public IEnumerable<Answer> WrongAnswers { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatorId { get; set; }

        public IdentityUser Creator { get; set; }
    }
}
