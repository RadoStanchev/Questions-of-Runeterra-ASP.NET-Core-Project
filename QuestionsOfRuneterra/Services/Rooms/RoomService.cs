using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QuestionsOfRuneterra.Data;
using QuestionsOfRuneterra.Data.Models;
using QuestionsOfRuneterra.Infrastructure.Extensions;
using QuestionsOfRuneterra.Models.Rooms;
using QuestionsOfRuneterra.Services.ApplicationUsers;
using System.Collections.Generic;
using System.Linq;

namespace QuestionsOfRuneterra.Services.Rooms
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext data;

        private readonly IConfigurationProvider mapper;

        private readonly IApplicationUserService applicationUserService;

        public RoomService(ApplicationDbContext data, IMapper mapper, IApplicationUserService applicationUserService)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
            this.applicationUserService = applicationUserService;
        }

        public string Add(string name, string ownerId, IEnumerable<ApplicationUser> members = null)
        {
            var room = new Room
            {
                Name = name,
                OwnerId = ownerId,
                Members = members
            };

            data.Rooms.Add(room);
            data.SaveChanges();

            return room.Id;
        }

        public RoomServiceModel Delete(string roomId)
        {
            var room = data.Rooms.FirstOrDefault(r => r.Id == roomId);

            if (room == null)
            {
                return null;
            }

            var roomModel = Room(roomId);

            data.Rooms.Remove(room);
            data.SaveChanges();

            return roomModel;
        }

        public bool Edit(string roomId, string name)
        {
            var room = data.Rooms.FirstOrDefault(r => r.Id == roomId);

            if(room == null)
            {
                return false;
            }

            room.Name = name;
            data.SaveChanges();

            return true;
        }

        public bool Exists(string roomId)
        {
            return data.Rooms.Any(r => r.Id == roomId);
        }

        public bool IsNameUnique(string roomName)
        {
            return data.Rooms.All(r => r.Name != roomName);
        }

        public bool IsOwnedBy(string roomId, string userId)
        {
            return data.Rooms.FirstOrDefault(r => r.Id == roomId).OwnerId == userId;
        }

        public string Name(string roomId)
        {
            return data.Rooms.FirstOrDefault(r => r.Id == roomId).Name;
        }

        public RoomServiceModel PrivateRoom(string senderId, string receiverId)
        {
            var senderName = applicationUserService.UserName(senderId);
            var receiverName = applicationUserService.UserName(receiverId);

            return Room(data.Rooms
                .FirstOrDefault(r => r.Name == $"{senderName}|{receiverName}"
                || r.Name == $"{receiverName}|{senderName}").Id);
        }

        public RoomServiceModel Room(string roomId)
        {
            return data.Rooms.ProjectToSingle<Room, RoomServiceModel>(r => r.Id == roomId, mapper);
        }

        public IEnumerable<RoomServiceModel> Rooms(bool[] states)
        {
            return data.Rooms
                    .Where(r => states.Contains(r.IsPublic))
                    .ProjectTo<RoomServiceModel>(mapper);
        }
    }
}
