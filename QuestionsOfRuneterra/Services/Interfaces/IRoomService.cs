using QuestionsOfRuneterra.Data.Models;
using System.Collections.Generic;

namespace QuestionsOfRuneterra.Services.Interfaces
{
    public interface IRoomService
    {
        string Add(string name, string ownerId, IEnumerable<ApplicationUser> members);
    }
}
