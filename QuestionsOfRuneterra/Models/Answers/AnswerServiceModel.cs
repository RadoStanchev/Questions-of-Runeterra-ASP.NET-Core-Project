using System.ComponentModel.DataAnnotations;
using static QuestionsOfRuneterra.Data.DataConstants.Answer;


namespace QuestionsOfRuneterra.Models.Answers
{
    public class AnswerServiceModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; }

        public bool IsRight { get; set; }

        public string QuestionId { get; set; }
    }
}
