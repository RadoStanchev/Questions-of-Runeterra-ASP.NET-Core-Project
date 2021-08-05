using System;
using System.Collections.Generic;

namespace QuestionsOfRuneterra.Data.Models
{
    public class Friendships
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public IEnumerable<ApplicationUser> Friends { get; set; }

        public string RoomId { get; set; }

        public Room Room { get; set; }
    }
}
