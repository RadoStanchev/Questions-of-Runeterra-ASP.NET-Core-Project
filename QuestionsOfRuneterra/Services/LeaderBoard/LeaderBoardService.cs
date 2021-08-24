using QuestionsOfRuneterra.Data;
using QuestionsOfRuneterra.Models.LeaderBoard;
using System.Collections.Generic;
using System.Linq;

namespace QuestionsOfRuneterra.Services.LeaderBoard
{
    public class LeaderBoardService : ILeaderBoardService
    {
        private readonly ApplicationDbContext data;

        public LeaderBoardService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<LeaderBoardUserServiceModel> Players(string searchTerm = null, int currentPage = 1, int playersPerPage = int.MaxValue)
        {
            var players = data.ApplicationUsers.OrderByDescending(p => p.QuizGames.Sum(qg => qg.Points)).AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                players = players.Where(p =>
                    (p.FirstName + " " + p.LastName).ToLower().Contains(searchTerm.ToLower()) ||
                    p.UserName.ToLower().Contains(searchTerm.ToLower()));
            }

            return players
                .Skip((currentPage - 1) * playersPerPage)
                .Take(playersPerPage)
                .Select(p => new LeaderBoardUserServiceModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Points = p.QuizGames.Sum(qg => qg.Points),
                ProfileImagePath = p.ProfileImagePath,
                UserName = p.UserName
            }).ToList();
        }
    }
}
