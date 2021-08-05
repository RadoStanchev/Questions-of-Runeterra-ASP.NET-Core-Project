using System;

namespace QuestionsOfRuneterra.Data.Models
{
    public class Message
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }

        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        public int ToRoomId { get; set; }
        public Room ToRoom { get; set; }
    }
}
