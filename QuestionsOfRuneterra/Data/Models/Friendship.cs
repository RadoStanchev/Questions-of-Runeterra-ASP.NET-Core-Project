using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuestionsOfRuneterra.Data.Models
{
    public class Friendship
    {
        [Required]
        public string FirstFriendId { get; set; }

        [Required]
        ApplicationUser FirstFriend { get; set; }

        [Required]
        public string SecondFriendId { get; set; }

        [Required]
        ApplicationUser SecondFriend { get; set; }

        [Required]
        public string RoomId { get; set; }

        [Required]
        public Room Room { get; set; }
    }
}
