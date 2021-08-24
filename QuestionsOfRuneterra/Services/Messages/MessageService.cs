using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QuestionsOfRuneterra.Data;
using QuestionsOfRuneterra.Data.Models;
using QuestionsOfRuneterra.Infrastructure.Extensions;
using QuestionsOfRuneterra.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace QuestionsOfRuneterra.Services.Messages
{
    public class MessageService : IMessageService
    {

        private readonly ApplicationDbContext data;

        private readonly IConfigurationProvider mapper;

        public MessageService(ApplicationDbContext data, IMapper mapper, Random rnd)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public string Add(string content, string toRoomId, string senderId)
        {
            var message = new Message()
            {
                Content = Regex.Replace(content, @"(?i)<(?!img|a|/a|/img).*?>", string.Empty),
                SenderId = senderId,
                ToRoomId = toRoomId,
                CreatedOn = DateTime.Now
            };

            data.Messages.Add(message);
            data.SaveChangesAsync();

            return message.Id;
        }


        public bool Delete(string messageId)
        {
            var message = data.Messages.FirstOrDefault(m => m.Id == messageId);

            if(message == null)
            {
                return false;
            }

            data.Messages.Remove(message);
            data.SaveChangesAsync();

            return true;
        }

        public bool Exists(string messageId)
        {
            return data.Messages.Any(m => m.Id == messageId);
        }

        public bool IsSentBy(string messageId, string userId)
        {
            return data.Messages.FirstOrDefault(m => m.Id == messageId).SenderId == userId;
        }

        public MessageServiceModel Message(string messageId)
        {
            return data.Messages.ProjectToSingle<Message, MessageServiceModel>(m => m.Id == messageId, mapper);
        }

        public IEnumerable<MessageServiceModel> MessagesToRoom(string roomId, int currentPage = 1,
            int messagesPerPage = int.MaxValue)
        {
            return data.Messages
                    .Include(m => m.Sender)
                    .Include(m => m.ToRoom)
                    .Where(m => m.ToRoomId == roomId)
                    .OrderByDescending(m => m.CreatedOn)
                    .Skip((currentPage - 1) * messagesPerPage)
                    .Take(messagesPerPage)
                    .Reverse()
                    .ProjectTo<MessageServiceModel>(mapper);
        }
    }
}
