using System;
using System.ComponentModel.DataAnnotations;

namespace QuestionsOfRuneterra.Data.Models
{
    public class QuizGameSession
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string QuestionId { get; set; }

        [Required]
        public Question Question { get; set; }

        [Required]
        public string SelectedAnswerId { get; set; }

        [Required]
        public Answer SelectedAnswer { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

    }
}
