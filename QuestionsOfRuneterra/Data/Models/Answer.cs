using System;
using System.ComponentModel.DataAnnotations;
using  static QuestionsOfRuneterra.Data.DataConstants.Answer;

namespace QuestionsOfRuneterra.Data.Models
{
    public class Answer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        public bool IsRight { get; set; }

        [Required]
        public string QuestionId { get; set; }

        [Required]
        public Question Question { get; set; }

        [Required]
        public string CreatorId { get; set; }

        [Required]
        public ApplicationUser Creator { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
