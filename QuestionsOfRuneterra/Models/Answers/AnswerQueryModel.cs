using System.ComponentModel.DataAnnotations;

namespace QuestionsOfRuneterra.Models.Answers
{
    public class AnswerQueryModel
    {
        public AnswerServiceModel CurrentAnswer { get; set; }

        public AnswerServiceModel PreviousAnswer { get; set; }

        public string QuestionId { get; set; }

        public int TotalAnswers { get; set; }

        public bool IncreaseOrderNumber { get; set; }
    }
}
