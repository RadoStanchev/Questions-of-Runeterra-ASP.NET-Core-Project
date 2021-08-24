using Microsoft.EntityFrameworkCore;
using QuestionsOfRuneterra.Data;
using QuestionsOfRuneterra.Data.Models;
using QuestionsOfRuneterra.Models.Questions;
using QuestionsOfRuneterra.Services.Answers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestionsOfRuneterra.Services.Questions
{
    public class QuestionService : IQuestionService
    {
        private readonly Random rnd;

        private readonly ApplicationDbContext data;

        private readonly IAnswerService answerService;

        public QuestionService(ApplicationDbContext data, IAnswerService answerService, Random rnd)
        {
            this.data = data;
            this.answerService = answerService;
            this.rnd = rnd;
        }

        public string Add(string content, string creatorId)
        {
            var question = new Question
            {
                Content = content,
                CreatorId = creatorId,
                CreatedOn = DateTime.Now,
                IsFeatured = false,
                IsPublic = false,
            };

            data.Questions.Add(question);
            data.SaveChanges();

            return question.Id;
        }

        public bool Delete(string questionId)
        {
            var question = data.Questions.FirstOrDefault(q => q.Id == questionId);
            if (question == null)
            {
                return false;
            }

            foreach (var answer in question.Answers)
            {
                answerService.Delete(answer.Id);
            }

            data.Questions.Remove(question);
            data.SaveChanges();

            return true;
        }

        public bool Edit(string questionId, string content)
        {
            var question = data.Questions.FirstOrDefault(q => q.Id == questionId);
            if (question == null)
            {
                return false;
            }

            question.Content = content;
            data.SaveChanges();

            return true;
        }

        public bool IsOwnedBy(string questionId, string userId)
        {
            return data.Questions.FirstOrDefault(q => q.Id == questionId).CreatorId == userId;
        }

        public QuestionServiceModel Question(string questionId)
        {
            return data.Questions
                .Where(q => q.Id == questionId)
                .Select(q => new QuestionServiceModel
                {
                    QuestionId = q.Id,
                    Content = q.Content
                })
                .FirstOrDefault();
        }

        public int RightAnswersCount(string questionId)
        {
            return AnswersCount(questionId, new bool[] { true });
        }

        public bool Save(string questionId)
        {
            var question = data.Questions.FirstOrDefault(q => q.Id == questionId);
            if(question == null)
            {
                return false;
            }

            question.IsFeatured = true;
            data.SaveChanges();

            return true;
        }

        public int WrongAnswersCount(string questionId)
        {
            return AnswersCount(questionId, new bool[] { false });
        }

        public int AnswersCount(string questionId, bool[] states)
        {
            return data.Questions
                .Include(q => q.Answers)
                .FirstOrDefault(q => q.Id == questionId)
                .Answers
                .Where(a => states.Contains(a.IsRight))
                .Count();
        }

        public bool DeleteUnfeatured(string userId)
        {
            var questions = data.Questions
                .Where(q => q.CreatorId == userId && q.IsFeatured == false);

            foreach (var question in questions)
            {
                Delete(question.Id);
            }

            return true;
        }

        public QuizGameSessionQuestionServiceModel RandomQuestion(IList<string> usedQuestionIds)
        {
            var question = data.Questions.Where(q => usedQuestionIds.Contains(q.Id) == false).ToArray()[rnd.Next(data.Questions.Count())];

            return new QuizGameSessionQuestionServiceModel
            {
                Content = question.Content,
                Id = question.Id,
                Answers = answerService.SetAnswers(question.Id),
            };
        }

        public bool IsAnswerRightToQuestion(string answerId, string questionId)
        {
            return answerService.IsAnswerToQuestion(answerId, questionId) && answerService.IsRight(answerId);
        }
    }
}
