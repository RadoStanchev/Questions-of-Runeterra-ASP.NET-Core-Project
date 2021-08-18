using QuestionsOfRuneterra.Models.QuizGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsOfRuneterra.Services.Interfaces
{
    public interface IQuizGameSessionService
    {
        QuizGameSessionServiceModel Make(string quizGameId);

        bool CanContinue(string answerId, string questionId);

        bool AddAnswer(string sessionId, string answerId);
    }
}
