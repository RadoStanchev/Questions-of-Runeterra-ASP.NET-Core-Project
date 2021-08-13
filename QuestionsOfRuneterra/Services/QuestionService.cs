using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestionsOfRuneterra.Data;
using QuestionsOfRuneterra.Data.Models;
using QuestionsOfRuneterra.Services.Interfaces;

namespace QuestionsOfRuneterra.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext data;

        public QuestionService(ApplicationDbContext data)
        {
            this.data = data;
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
    }
}
