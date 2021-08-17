using QuestionsOfRuneterra.Models.Friendships;
using System.Collections.Generic;

namespace QuestionsOfRuneterra.Services.Interfaces
{
    public interface IFriendshipService
    {
        IEnumerable<SuggestionServiceModel> Suggestions(string userId);

        bool AreFriends(string firstUserId, string secondUserId);

        bool Add(string firstUserId, string secondUserId);
    }
}
