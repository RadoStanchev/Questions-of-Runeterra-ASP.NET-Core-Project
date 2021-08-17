using QuestionsOfRuneterra.Models.Answers;
using System.Collections.Generic;

namespace QuestionsOfRuneterra.Services.Interfaces
{
    public interface IAnswerService
    {
        string Add(string content, bool isRight, string questionId, string creatorId);

        bool Edit(string answerId, string content, bool isRight);

        AnswerServiceModel PreviousAnswer(string questionId, int orderNumber);

        AnswerServiceModel NextAnswer(string questionId, int orderNumber);

        bool IsAnswerToQuestion(string answerId, string questionId);

        int TotalAnswersToQuestion(string questionId);

        bool IsOwnedBy(string answerId, string userId);

        bool Delete(string answerId);

        bool IsRight(string answerId);

        IEnumerable<QuizGameSessionAnswerServiceModel> SetAnswers(string questionId);
    }
}
