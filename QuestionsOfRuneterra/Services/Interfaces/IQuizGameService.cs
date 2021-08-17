using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsOfRuneterra.Services.Interfaces
{
    public interface IQuizGameService
    {
        string Start(string playerId);

        string Stop(string gameId);

        bool IsPlayedBy(string gameId, string userId);
    }
}
