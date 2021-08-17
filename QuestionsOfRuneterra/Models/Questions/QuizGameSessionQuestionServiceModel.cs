using QuestionsOfRuneterra.Models.Answers;
using System.Collections.Generic;

namespace QuestionsOfRuneterra.Models.Questions
{
    public class QuizGameSessionQuestionServiceModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public IEnumerable<QuizGameSessionAnswerServiceModel> Answers { get; set; }
    }
}
