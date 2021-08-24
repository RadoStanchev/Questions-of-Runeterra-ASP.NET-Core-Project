using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsOfRuneterra.Models.Friendships
{
    public class AllFriendshipQueryModel
    {
        public IEnumerable<SuggestionServiceModel> Suggestions { get; set; }

        public IEnumerable<FriendServiceModel> Friends { get; set; }
    }
}
