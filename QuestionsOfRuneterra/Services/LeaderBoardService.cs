using QuestionsOfRuneterra.Data;
using QuestionsOfRuneterra.Models.LeaderBoard;
using QuestionsOfRuneterra.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsOfRuneterra.Services
{
    public class LeaderBoardService : ILeaderBoardService
    {
        private readonly ApplicationDbContext data;

        public LeaderBoardService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<LeaderBoardUserServiceModel> Leaders()
        {
            throw new NotImplementedException();
        }
    }
}
