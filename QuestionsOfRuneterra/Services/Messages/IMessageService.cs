using QuestionsOfRuneterra.Models.Messages;
using System.Collections.Generic;

namespace QuestionsOfRuneterra.Services.Messages
{
    public interface IMessageService
    {
        bool Exists(string messageId);

        MessageServiceModel Message(string messageId);

        IEnumerable<MessageServiceModel> MessagesToRoom(string roomId, int currentPage = 1,
            int messagesPerPage = int.MaxValue);

        string Add(string content, string toRoomId, string senderId);

        bool IsSentBy(string messageId, string userId);

        bool Delete(string messageId);
    }
}
