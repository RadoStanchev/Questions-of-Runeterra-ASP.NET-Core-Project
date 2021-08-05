using System;

namespace QuestionsOfRuneterra.Data.Models
{
    public class QuizGameSession
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string QuestionId { get; set; }

        public Question Question { get; set; }

        public string AnswerId { get; set; }

        public Answer Answer { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
