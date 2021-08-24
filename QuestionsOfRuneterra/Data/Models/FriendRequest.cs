using System;
using System.ComponentModel.DataAnnotations;

namespace QuestionsOfRuneterra.Data.Models
{
    public class FriendRequest
    {
        [Required]
        public string SenderId { get; set; }

        [Required]
        ApplicationUser Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        [Required]
        ApplicationUser Receiver { get; set; }

        public DateTime SendOn { get; set; }

        public bool Approved { get; set; }
    }
}
