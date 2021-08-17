using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestionsOfRuneterra.Data;
using QuestionsOfRuneterra.Data.Models;
using QuestionsOfRuneterra.Models.Questions;
using QuestionsOfRuneterra.Services.Interfaces;

namespace QuestionsOfRuneterra.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext data;

        private readonly IAnswerService answerService;

        public QuestionService(ApplicationDbContext data, IAnswerService answerService = null)
        {
            this.data = data;
            this.answerService = answerService;
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

        public bool isOwnedBy(string questionId, string userId)
        {
            return data.Questions.FirstOrDefault(q => q.Id == questionId).CreatorId == userId;
        }

        public QuestionServiceModel Question(string questionId)
        {
            return data.Questions
                .Select(q => new QuestionServiceModel
                {
                    Id = q.Id,
                    Content = q.Content
                })
                .FirstOrDefault(q => q.Id == questionId);
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
    }
}
