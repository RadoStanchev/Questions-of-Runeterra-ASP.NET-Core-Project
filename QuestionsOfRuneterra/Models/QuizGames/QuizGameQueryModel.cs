using QuestionsOfRuneterra.Services.QuizGame;
using System.Collections.Generic;

namespace QuestionsOfRuneterra.Models.QuizGames
{
    public class QuizGameQueryModel 
    {
        public const int GamesPerPage = 6;

        public GameSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalGames { get; set; }

        public IEnumerable<QuizGameServiceModel> Games { get; set; }
    }
}
