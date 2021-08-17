using QuestionsOfRuneterra.Models.Answers;
using QuestionsOfRuneterra.Models.Questions;
using System.Collections.Generic;

namespace QuestionsOfRuneterra.Models.QuizGames
{
    public class QuizGameSessionServiceModel
    {
        public string OrderNumber { get; set; }

        public string Id { get; set; }

        public QuizGameSessionQuestionServiceModel Question { get; set; }
    }
}
