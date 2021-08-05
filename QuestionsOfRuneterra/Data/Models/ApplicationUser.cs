using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace QuestionsOfRuneterra.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Avatar { get; set; }

        public IEnumerable<Room> Rooms { get; set; }

        public IEnumerable<Message> Messages { get; set; }

        public IEnumerable<ApplicationUser> Friends { get; set; }
    }
}
