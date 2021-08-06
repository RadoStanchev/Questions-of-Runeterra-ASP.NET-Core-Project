using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static QuestionsOfRuneterra.Data.DataConstants.Question;

namespace QuestionsOfRuneterra.Data.Models
{
    public class Question
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public IEnumerable<Answer> Answers { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string CreatorId { get; set; }

        [Required]
        public ApplicationUser Creator { get; set; }

        [Required]
        public IEnumerable<QuizGameSession> Sessions { get; set; }
    }
}
