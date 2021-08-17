using QuestionsOfRuneterra.Models.Friendships;
using System.Collections.Generic;

namespace QuestionsOfRuneterra.Services.Interfaces
{
    public interface IApplicationUserService
    {
        IEnumerable<string> GetProfileImagePaths();

        bool IsUsernameUnique(string username);

        string AdminId();

        string UserName(string userId);
    }
}
