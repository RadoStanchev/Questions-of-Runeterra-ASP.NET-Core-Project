using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuestionsOfRuneterra.Data;
using QuestionsOfRuneterra.Data.Models;
using QuestionsOfRuneterra.Models.Answers;
using QuestionsOfRuneterra.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace QuestionsOfRuneterra.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly Random rnd;

        private readonly ApplicationDbContext data;

        private readonly IConfigurationProvider mapper;

        public AnswerService(ApplicationDbContext data, IMapper mapper, Random rnd)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
            this.rnd = rnd;
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

        public bool Delete(string answerId)
        {
            var answer = data.Answers.FirstOrDefault(a => a.Id == answerId);

            if (answer == null)
            {
                return false;
            }

            data.Answers.Remove(answer);
            data.SaveChanges();

            return true;
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

        public bool Exists(string answerId)
        {
            return data.Answers.Any(a => a.Id == answerId);
        }

        public bool IsAnswerToQuestion(string answerId, string questionId)
        {
            return data.Answers.FirstOrDefault(a => a.Id == answerId).QuestionId == questionId;
        }

        public bool IsOwnedBy(string answerId, string userId)
        {
            return data.Answers.FirstOrDefault(a => a.Id == answerId).CreatorId == userId;
        }

        public bool IsRight(string answerId)
        {
            return data.Answers.FirstOrDefault(a => a.Id == answerId).IsRight;
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
                            Id = a.Id,
                            Content = a.Content,
                            IsRight = a.IsRight,
                            QuestionId = questionId,
                        })
                        .Skip(orderNumber)
                        .Take(1)
                        .First();
        }

        public IEnumerable<QuizGameSessionAnswerServiceModel> SetAnswers(string questionId)
        {
            var question = data.Questions.FirstOrDefault(q => q.Id == questionId);

            var rightAnswers = question.Answers.Where(a => a.IsRight).ToArray();
            var rightAnswer = rightAnswers[rnd.Next(rightAnswers.Count())];

            var answers = new List<QuizGameSessionAnswerServiceModel>()
            {
                new QuizGameSessionAnswerServiceModel
                {
                    Content = rightAnswer.Content,
                    Id = rightAnswer.Id,
                },
            };

            var wrongAnswers = question.Answers.Where(a => a.IsRight == false).ToArray();

            var usedWrongAnswers = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                var unusedWrongAnswers = wrongAnswers.Where(a => usedWrongAnswers.Contains(a.Id) == false).ToArray();
                var wrongAnswer = unusedWrongAnswers[rnd.Next(unusedWrongAnswers.Count())];

                answers.Add(new QuizGameSessionAnswerServiceModel
                {
                    Content = wrongAnswer.Content,
                    Id = wrongAnswer.Id,
                });

                usedWrongAnswers.Add(wrongAnswer.Id);
            }

            return answers;
        }

        public int TotalAnswersToQuestion(string questionId)
        {
            return data.Questions.Include(q => q.Answers).FirstOrDefault(q => q.Id == questionId).Answers.Count();
        }
    }
}
