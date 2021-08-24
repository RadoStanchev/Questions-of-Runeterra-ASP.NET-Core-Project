using System;
using System.ComponentModel.DataAnnotations;
using static QuestionsOfRuneterra.Data.DataConstants.Message;

namespace QuestionsOfRuneterra.Data.Models
{
    public class Message
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string SenderId { get; set; }

        [Required]
        public ApplicationUser Sender { get; set; }

        [Required]
        public string ToRoomId { get; set; }

        [Required]
        public Room ToRoom { get; set; }
    }
}
