using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuestionsOfRuneterra.Data.Models
{
    public class Friendship
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public IEnumerable<ApplicationUser> Friends { get; set; }

        [Required]
        public string RoomId { get; set; }

        [Required]
        public Room Room { get; set; }
    }
}
