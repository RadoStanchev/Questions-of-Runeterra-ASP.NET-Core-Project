using Microsoft.AspNetCore.Identity;
using System;

namespace QuestionsOfRuneterra.Data.Models
{
    public class Answer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Content { get; set; }

        public string QuestionId { get; set; }

        public Question Question { get; set; }

        public string CreatorId { get; set; }

        public IdentityUser Creator { get; set; }
    }
}
