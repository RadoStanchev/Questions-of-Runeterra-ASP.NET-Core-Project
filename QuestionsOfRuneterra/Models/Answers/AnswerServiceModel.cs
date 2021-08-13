using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
