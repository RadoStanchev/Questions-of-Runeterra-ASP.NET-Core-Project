using System.Collections.Generic;

namespace QuestionsOfRuneterra.Services.Interfaces
{
    public interface IApplicationUserService
    {
        IEnumerable<string> GetProfileImagePaths();

        bool isUsernameUnique(string username);
    }
}
