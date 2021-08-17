using AutoMapper;
using QuestionsOfRuneterra.Data;
using QuestionsOfRuneterra.Data.Models;
using QuestionsOfRuneterra.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsOfRuneterra.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext data;

        private readonly IConfigurationProvider mapper;

        public RoomService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public string Add(string name, string ownerId, IEnumerable<ApplicationUser> members)
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
    }
}
