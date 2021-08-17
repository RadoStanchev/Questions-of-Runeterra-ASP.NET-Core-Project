using QuestionsOfRuneterra.Data;
using QuestionsOfRuneterra.Data.Models;
using QuestionsOfRuneterra.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsOfRuneterra.Services
{
    public class QuizGameService : IQuizGameService
    {
        private readonly ApplicationDbContext data;

        public QuizGameService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public bool IsPlayedBy(string gameId, string userId)
        {
            return data.QuizGames.FirstOrDefault(qg => qg.Id == gameId).PlayerId == userId;
        }

        public string Start(string playerId)
        {
            var quizGame = new QuizGame
            {
                PlayerId = playerId,
                StartedOn = DateTime.Now,
            };

            data.QuizGames.Add(quizGame);
            data.SaveChanges();

            return quizGame.Id;
        }

        public string Stop(string gameId)
        {
            var game = data.QuizGames.FirstOrDefault(qg => qg.Id == gameId);
            game.FinishedOn = DateTime.Now;
            data.SaveChanges();
        }
    }
}
