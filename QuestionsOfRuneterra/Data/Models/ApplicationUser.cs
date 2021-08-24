using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static QuestionsOfRuneterra.Data.DataConstants.ApplicationUser;

namespace QuestionsOfRuneterra.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(ProfileImagePathMaxLength)]
        public string ProfileImagePath { get; set; }

        [Required]
        public IEnumerable<Room> OwnedRooms { get; set; }

        [Required]
        public IEnumerable<QuizGame> QuizGames { get; set; }

        [Required]
        public IEnumerable<Room> JoinedRooms { get; set; }

        [Required]
        public IEnumerable<Message> Messages { get; set; }

        [Required]
        public IEnumerable<Friendship> Friendships { get; set; }

        [Required]
        public IEnumerable<FriendRequest> FriendRequests { get; set; }
    }
}
