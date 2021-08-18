using QuestionsOfRuneterra.Models.LeaderBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsOfRuneterra.Services.Interfaces
{
    public interface ILeaderBoardService
    {
        IEnumerable<LeaderBoardUserServiceModel> Players (string searchTerm, int currentPage, int playersPerPage);
    }
}
