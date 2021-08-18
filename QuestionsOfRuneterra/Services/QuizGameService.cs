using QuestionsOfRuneterra.Data;
using QuestionsOfRuneterra.Data.Models;
using QuestionsOfRuneterra.Models.QuizGames;
using QuestionsOfRuneterra.Services.Enums;
using QuestionsOfRuneterra.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestionsOfRuneterra.Services
{
    public class QuizGameService : IQuizGameService
    {
        private readonly ApplicationDbContext data;

        public QuizGameService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public bool AddPoints(string gameId, int amount)
        {
            var game = data.QuizGames.FirstOrDefault(qg => qg.Id == gameId);

            if (game == null)
            {
                return false;
            }

            game.Points += amount;
            data.SaveChanges();

            return true;
        }

        public QuizGameServiceModel Details(string gameId)
        {
            var game = data.QuizGames.FirstOrDefault(qg => qg.Id == gameId);


            return new QuizGameServiceModel
            {
                Id = game.Id,
                PlayerName = game.Player.UserName,
                Points = game.Points
            };
        }

        public bool IsFinished(string gameId)
        {
            return data.QuizGames.FirstOrDefault(qg => qg.Id == gameId).FinishedOn != null;
        }

        public bool IsPlayedBy(string gameId, string userId)
        {
            return data.QuizGames.FirstOrDefault(qg => qg.Id == gameId).PlayerId == userId;
        }

        public bool IsThereMoreQuestions(string gameId)
        {
            return data.Questions.Count() > UsedQuestionIds(gameId).Count();
        }

        public IEnumerable<QuizGameServiceModel> MyGames(
            string playerId,
            GameSorting sorting = GameSorting.StartedOn,
            int currentPage = 1,
            int gamesPerPage = int.MaxValue)
        {
            var games = this.data.QuizGames
                .Where(qg => qg.FinishedOn != null)
                .Where(qg => qg.PlayerId == playerId);

            games = sorting switch
            {
                GameSorting.FinishedOn => games.OrderByDescending(qg => qg.FinishedOn),
                GameSorting.Points => games.OrderByDescending(qg => qg.Points),
                GameSorting.StartedOn or _ => games.OrderByDescending(qg => qg.StartedOn)
            };

            return games.Select(g => new QuizGameServiceModel
            {
                Id = g.Id,
                PlayerName = g.Player.UserName,
                Points = g.Points
            }).ToList();
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

        public bool Stop(string gameId)
        {
            var game = data.QuizGames.FirstOrDefault(qg => qg.Id == gameId);

            if (game == null)
            {
                return false;
            }

            game.FinishedOn = DateTime.Now;
            data.SaveChanges();

            return true;
        }

        public IEnumerable<string> UsedQuestionIds(string gameId)
        {
            return data.QuizGames.FirstOrDefault(qg => qg.Id == gameId).Sessions.Select(qgs => qgs.QuestionId);
        }
    }
}
