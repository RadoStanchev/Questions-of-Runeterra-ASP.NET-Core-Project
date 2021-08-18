using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuestionsOfRuneterra.Data;
using QuestionsOfRuneterra.Data.Models;
using QuestionsOfRuneterra.Models.Friendships;
using QuestionsOfRuneterra.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace QuestionsOfRuneterra.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly ApplicationDbContext data;

        private readonly IConfigurationProvider mapper;

        private readonly IApplicationUserService applicationUserService;

        private readonly IRoomService roomService;

        public FriendshipService(ApplicationDbContext data, IMapper mapper, IApplicationUserService applicationUserService, IRoomService roomService)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
            this.applicationUserService = applicationUserService;
            this.roomService = roomService;
        }

        public bool Add(string firstUserId, string secondUserId)
        {
            var firstUser = data.ApplicationUsers.FirstOrDefault(au => au.Id == firstUserId);
            var secondUser = data.ApplicationUsers.FirstOrDefault(au => au.Id == secondUserId);

            var roomName = $"{firstUser.UserName}{secondUser.UserName}";

            var friendship = new Friendship
            {
                FirstFriendId = firstUserId,
                SecondFriendId = secondUserId,
                RoomId = roomService.Add(roomName, applicationUserService.AdminId(), new List<ApplicationUser>() { firstUser, secondUser })
            };

            return true;
        }

        public bool AreFriends(string firstUserId, string secondUserId)
        {
            return data.Friendships.Any(fs => (fs.FirstFriendId == firstUserId && fs.SecondFriendId == secondUserId) || (fs.FirstFriendId == secondUserId && fs.SecondFriendId == firstUserId));
        }

        public IEnumerable<SuggestionServiceModel> Suggestions(string userId)
        {
            var friendsIds = data.Friendships
                .Where(fs => fs.FirstFriendId == userId || fs.SecondFriendId == userId)
                .Select(fs => fs.FirstFriendId == userId ? fs.SecondFriendId : fs.FirstFriendId).ToList();

            return data.ApplicationUsers
                .Include(au => au.Friendships)
                .Where(au => friendsIds.Contains(au.Id) == false && au.Id != userId)
                .Where(au => au.Friendships.Any(fs => friendsIds.Contains(fs.FirstFriendId) || friendsIds.Contains(fs.SecondFriendId)))
                .Select(au => new SuggestionServiceModel
                {
                    Id = au.Id,
                    FirstName = au.FirstName,
                    LastName = au.LastName,
                    ProfileImagePath = au.ProfileImagePath,
                    CommonFriends = au.Friendships.Where(fs => friendsIds.Contains(fs.FirstFriendId) || friendsIds.Contains(fs.SecondFriendId)).Count()
                })
                .OrderByDescending(x => x.CommonFriends);
        }
    }
}
