using Microsoft.AspNetCore.SignalR;
using QuestionsOfRuneterra.Data.Models;
using QuestionsOfRuneterra.Infrastructure.Extensions;
using QuestionsOfRuneterra.Models.ChatHub;
using QuestionsOfRuneterra.Services.ApplicationUsers;
using QuestionsOfRuneterra.Services.Messages;
using QuestionsOfRuneterra.Services.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuestionsOfRuneterra.Hubs
{
    public class ChatHub : Hub
    {
        private readonly static Dictionary<string, UserServiceModel> ConnectionsMap = new Dictionary<string, UserServiceModel>();

        private readonly IRoomService roomService;
        private readonly IMessageService messageService;
        private readonly IApplicationUserService applicationUserService;

        public ChatHub(IMessageService messageService, IRoomService roomService)
        {
            this.messageService = messageService;
            this.roomService = roomService;
        }

        public void SendPrivate(string receiverId, string messageContent)
        {
            if (string.IsNullOrEmpty(messageContent.Trim()))
            {
                return;
            }

            if (!ConnectionsMap.ContainsKey(receiverId))
            {
                return;
            }

            var senderId = User.Id();

            var room = roomService.PrivateRoom(senderId, receiverId);
            var message = messageService.Message(messageService.Add(messageContent, room.Id, senderId));

            // Send the message
            Clients.Group(room.Name).SendAsync("newMessage", message);
        }

        public void Join(string roomId)
        {
            try
            {
                var user = ConnectionsMap.Where(kvp => kvp.Key == User.Id()).FirstOrDefault().Value;
                if (user != null && user.CurrentRoomId != roomId)
                {
                    if (!string.IsNullOrEmpty(user.CurrentRoomId))
                        Clients.OthersInGroup(roomService.Name(user.CurrentRoomId)).SendAsync("removeUser", user);

                    Leave(roomService.Name(user.CurrentRoomId));
                    Groups.AddToGroupAsync(Context.ConnectionId, roomService.Name(roomId));
                    user.CurrentRoomId = roomId;

                    Clients.OthersInGroup(roomService.Name(roomId)).SendAsync("addUser", user);
                }
            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("onError", "You failed to join the chat room!" + ex.Message);
            }
        }

        public async Task Leave(string roomName)
        {
            Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public IEnumerable<UserServiceModel> GetUsers(string roomId)
        {
            return ConnectionsMap.Where(kvp => kvp.Value.CurrentRoomId == roomId).Select(kvp => kvp.Value).ToList(); ;
        }

        public override Task OnConnectedAsync()
        {
            try
            {
                var user = applicationUserService.User(User.Id());
                user.Device = GetDevice();

                if (!ConnectionsMap.Any(kvp => kvp.Key == User.Id()))
                {
                    ConnectionsMap.Add(User.Id(), user);
                }

                Clients.Caller.SendAsync("getProfileInfo", user.Username, user.ProfileImagePath);
            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("onError", "OnConnected:" + ex.Message);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var user = ConnectionsMap.Where(kvp => kvp.Key == User.Id()).Select(kvp => kvp.Value).First();
                ConnectionsMap.Remove(User.Id());

                Clients.OthersInGroup(roomService.Name(user.CurrentRoomId)).SendAsync("removeUser", user);
            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("onError", "OnDisconnected: " + ex.Message);
            }

            return base.OnDisconnectedAsync(exception);
        }

        private ClaimsPrincipal User => Context.User;

        private string GetDevice()
        {
            var device = Context.GetHttpContext().Request.Headers["Device"].ToString();
            if (!string.IsNullOrEmpty(device) && (device.Equals("Desktop") || device.Equals("Mobile")))
                return device;

            return "Web";
        }
    }
}
