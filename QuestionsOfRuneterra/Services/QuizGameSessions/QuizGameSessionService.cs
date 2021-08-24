using QuestionsOfRuneterra.Data;
using QuestionsOfRuneterra.Data.Models;
using QuestionsOfRuneterra.Models.QuizGames;
using QuestionsOfRuneterra.Services.Questions;
using System;
using System.Linq;

namespace QuestionsOfRuneterra.Services.QuizGameSessions
{
    public class QuizGameSessionService : IQuizGameSessionService
    {
        private readonly IQuestionService questionService;

        private readonly ApplicationDbContext data;

        public QuizGameSessionService(IQuestionService questionService, ApplicationDbContext data)
        {
            this.questionService = questionService;
            this.data = data;
        }

        public bool AddAnswer(string sessionId, string answerId)
        {
            var session = data.QuizGameSessions.FirstOrDefault(qgs => qgs.Id == sessionId);

            if (session == null)
            {
                return false;
            }

            session.SelectedAnswerId = answerId;
            data.SaveChanges();

            return true;
        }

        public bool CanContinue(string answerId , string questionId)
        {
            return questionService.IsAnswerRightToQuestion(answerId, questionId);
        }

        public QuizGameSessionServiceModel Make(string quizGameId)
        {
            var usedQuestionIds = data.QuizGames.FirstOrDefault(qg => qg.Id == quizGameId).Sessions.Select(qgs => qgs.QuestionId).ToList();

            var question = questionService.RandomQuestion(usedQuestionIds);

            var quizGameSession = new QuizGameSession
            {
                CreatedOn = DateTime.Now,
                QuestionId = question.Id,
                QuizGameId = quizGameId,
            };

            data.QuizGameSessions.Add(quizGameSession);
            data.SaveChanges();

            return new QuizGameSessionServiceModel
            {
                Id = quizGameSession.Id,
                Question = question,
            };
        }
    }
}
