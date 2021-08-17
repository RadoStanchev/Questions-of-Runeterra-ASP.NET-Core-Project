using QuestionsOfRuneterra.Models.Answers;

namespace QuestionsOfRuneterra.Services.Interfaces
{
    public interface IAnswerService
    {
        string Add(string content, bool isRight, string questionId, string creatorId);

        bool Edit(string answerId, string content, bool isRight);

        AnswerServiceModel PreviousAnswer(string questionId, int orderNumber);

        AnswerServiceModel NextAnswer(string questionId, int orderNumber);

        bool isAnswerToQuestion(string answerId, string questionId);

        int TotalAnswersToQuestion(string questionId);

        bool isOwnedBy(string answerId, string userId);

        bool Delete(string answerId);
    }
}
