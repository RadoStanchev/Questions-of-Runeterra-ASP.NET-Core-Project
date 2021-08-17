using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsOfRuneterra.Models.Friendships
{
    public class SuggestionServiceModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CommonFriends { get; set; }

        public string ProfileImagePath { get; set; }
    }
}
