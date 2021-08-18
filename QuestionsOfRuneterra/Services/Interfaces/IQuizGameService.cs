using QuestionsOfRuneterra.Models.QuizGames;
using QuestionsOfRuneterra.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsOfRuneterra.Services.Interfaces
{
    public interface IQuizGameService
    {
        string Start(string playerId);

        bool Stop(string gameId);

        bool IsPlayedBy(string gameId, string userId);

        IEnumerable<string> UsedQuestionIds(string gameId);

        bool IsThereMoreQuestions(string gameId);

        bool IsFinished(string gameId);

        QuizGameServiceModel Details(string gameId);

        bool AddPoints(string gameId, int amount);

        IEnumerable<QuizGameServiceModel> MyGames(
            string playerId,
            GameSorting sorting = GameSorting.StartedOn,
            int currentPage = 1,
            int gamesPerPage = int.MaxValue);
    }
}
