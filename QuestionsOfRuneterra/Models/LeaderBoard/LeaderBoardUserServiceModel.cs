using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsOfRuneterra.Models.LeaderBoard
{
    public class LeaderBoardUserServiceModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfileImagePath { get; set; }

        public int Points { get; set; }
    }
}
