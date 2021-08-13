using QuestionsOfRuneterra.Models.Answers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static QuestionsOfRuneterra.Data.DataConstants.Question;


namespace QuestionsOfRuneterra.Models.Questions
{
    public class QuestionFormModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; }

        public IEnumerable<AnswerServiceModel> Answers { get; set; }
    }
}
