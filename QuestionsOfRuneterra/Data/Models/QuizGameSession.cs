using System;
using System.ComponentModel.DataAnnotations;

namespace QuestionsOfRuneterra.Data.Models
{
    public class QuizGameSession
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string QuizGameId { get; set; }

        public QuizGame QuizGame { get; set; }

        [Required]
        public string QuestionId { get; set; }

        [Required]
        public Question Question { get; set; }

        public string SelectedAnswerId { get; set; }

        public Answer SelectedAnswer { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

    }
}
