using Microsoft.AspNetCore.Mvc;
using QuestionsOfRuneterra.Services.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
