using Microsoft.AspNetCore.Mvc;
using QuestionsOfRuneterra.Models.LeaderBoard;
using QuestionsOfRuneterra.Services.Interfaces;
using System.Linq;

namespace QuestionsOfRuneterra.Controllers
{
    public class LeaderBoardController : Controller
    {
        private readonly ILeaderBoardService leaderBoardService;

        public LeaderBoardController(ILeaderBoardService leaderBoardService)
        {
            this.leaderBoardService = leaderBoardService;
        }

        public IActionResult All([FromQuery] LeaderBoardQueryModel query)
        {
            var players = this.leaderBoardService.Players(
                query.SearchTerm,
                query.CurrentPage,
                LeaderBoardQueryModel.PlayersPerPage);

            query.Players = players;
            query.TotalPlayers = players.Count();
            return View(query);
        }
    }
}
