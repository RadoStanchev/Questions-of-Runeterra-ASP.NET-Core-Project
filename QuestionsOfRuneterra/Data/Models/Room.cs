using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static QuestionsOfRuneterra.Data.DataConstants.Room;

namespace QuestionsOfRuneterra.Data.Models
{
    public class Room
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string OwnerId { get; set; }

        [Required]
        public ApplicationUser Owner { get; set; }

        [Required]
        public IEnumerable<Message> Messages { get; set; }

        [Required]
        public IEnumerable<ApplicationUser> Members { get; set; }
    }
}
