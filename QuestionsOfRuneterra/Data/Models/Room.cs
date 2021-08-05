using System;
using System.Collections.Generic;

namespace QuestionsOfRuneterra.Data.Models
{
    public class Room
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
