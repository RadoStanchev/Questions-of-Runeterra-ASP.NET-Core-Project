using QuestionsOfRuneterra.Models.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsOfRuneterra.Services.Interfaces
{
    public interface IQuestionService
    {
        string Add(string content, string creatorId);

        bool IsOwnedBy(string questionId, string userId);

        bool Edit(string questionId, string content);

        bool Save(string questionId);

        int RightAnswersCount(string questionId);

        int WrongAnswersCount(string questionId);

        QuestionServiceModel Question(string questionId);

        bool Delete(string questionId);

        bool DeleteUnfeatured(string userId);

        QuizGameSessionQuestionServiceModel RandomQuestion(IList<string> usedQuestionIds);

        bool IsAnswerRightToQuestion(string answerId, string questionId);
    }
}
