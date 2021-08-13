using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuestionsOfRuneterra.Data;
using QuestionsOfRuneterra.Data.Models;
using QuestionsOfRuneterra.Models.Answers;
using QuestionsOfRuneterra.Services.Interfaces;
using System;
using System.Linq;


namespace QuestionsOfRuneterra.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly ApplicationDbContext data;

        private readonly IConfigurationProvider mapper;

        public AnswerService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public string Add(string content, bool isRight, string questionId, string creatorId)
        {
            var answer = new Answer
            {
                Content = content,
                IsRight = isRight,
                QuestionId = questionId,
                CreatedOn = DateTime.Now,
                CreatorId = creatorId,
            };

            data.Answers.Add(answer);
            data.SaveChanges();

            return answer.Id;
        }

        public bool Edit(string answerId, string content, bool isRight)
        {
            var answer = data.Answers.FirstOrDefault(a => a.Id == answerId);

            if(answer == null)
            {
                return false;
            }

            answer.Content = answerId;
            answer.IsRight = isRight;
            data.SaveChanges();

            return true;
        }

        public bool isAnswerToQuestion(string answerId, string questionId)
        {
            return data.Answers.FirstOrDefault(a => a.Id == answerId).QuestionId == questionId;
        }

        public AnswerServiceModel NextAnswer(string questionId, int orderNumber)
        {
            return PreviousAnswer(questionId, orderNumber + 1);
        }

        public AnswerServiceModel PreviousAnswer(string questionId, int orderNumber)
        {
            return data.Questions
                .Include(q => q.Answers)
                .FirstOrDefault(q => q.Id == questionId)
                    .Answers
                        .OrderBy(a => a.CreatedOn)
                        .Select(a => new AnswerServiceModel
                        {
                            Content = a.Content,
                            IsRight = a.IsRight,
                            QuestionId = questionId,
                        })
                        .Skip(orderNumber)
                        .Take(1)
                        .First();
        }

        public int TotalAnswersToQuestion(string questionId)
        {
            return data.Questions.Include(q => q.Answers).FirstOrDefault(q => q.Id == questionId).Answers.Count();
        }
    }
}
