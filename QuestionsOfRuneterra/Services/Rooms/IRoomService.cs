using QuestionsOfRuneterra.Data.Models;
using QuestionsOfRuneterra.Models.Rooms;
using System.Collections.Generic;

namespace QuestionsOfRuneterra.Services.Rooms
{
    public interface IRoomService
    {
        string Add(string name, string ownerId, IEnumerable<ApplicationUser> members = null);

        bool Edit(string roomId, string name);

        RoomServiceModel Delete(string roomId);

        bool Exists(string roomId);

        string Name(string roomId);

        RoomServiceModel PrivateRoom(string senderId, string receiverId);

        bool IsNameUnique(string roomName);

        bool IsOwnedBy(string roomId, string userId);

        IEnumerable<RoomServiceModel> Rooms(bool[] states);

        RoomServiceModel Room(string roomId);
    }
}
