using System.ComponentModel.DataAnnotations;

namespace QuestionsOfRuneterra.Models.Messages
{
    public class MessageServiceModel
    {
        [Required]
        public string Content { get; set; }

        public string CreatedOn { get; set; }

        public string SenderId { get; set; }

        [Required]
        public string ToRoomId { get; set; }

        public string UserImagePath { get; set; }
    }
}
