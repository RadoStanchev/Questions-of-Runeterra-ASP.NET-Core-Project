using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuestionsOfRuneterra.Models.LeaderBoard
{
    public class LeaderBoardQueryModel
    {
        public const int CarsPerPage = 20;

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalPages { get; set; }

        public IEnumerable<LeaderBoardUserServiceModel> Leaders { get; set; }

    }
}
