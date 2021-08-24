using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QuestionsOfRuneterra.Data.Models;
using QuestionsOfRuneterra.Hubs;
using QuestionsOfRuneterra.Infrastructure.Extensions;
using QuestionsOfRuneterra.Models.Messages;
using QuestionsOfRuneterra.Services.Messages;
using QuestionsOfRuneterra.Services.Rooms;
using System.Collections.Generic;
using System.Linq;

namespace QuestionsOfRuneterra.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IHubContext<ChatHub> hubContext;

        private readonly IMessageService messageService;

        private readonly IRoomService roomService;

        public MessagesController(IHubContext<ChatHub> hubContext, IMessageService messageService, IRoomService roomService)
        {
            this.hubContext = hubContext;
            this.messageService = messageService;
            this.roomService = roomService;
        }

        [HttpGet("{id}")]
        public ActionResult<Room> Get(string messageId)
        {
            if (messageService.Exists(messageId))
                return NotFound();

            return Ok(messageService.Message(messageId));
        }

        [HttpGet("Room/{roomId}")]
        public IActionResult GetMessages(string roomId)
        {
            if (roomService.Exists(roomId))
                return BadRequest();

            return Ok(messageService.MessagesToRoom(roomId));
        }

        [HttpPost]
        public ActionResult<Message> Add(MessageServiceModel message)
        {
            if (roomService.Exists(message.ToRoomId) == false)
                return BadRequest();

            var messageId = messageService.Add(message.Content, message.ToRoomId, User.Id());
            message = messageService.Message(messageId);

            hubContext.Clients.Group(roomService.Name(message.ToRoomId)).SendAsync("newMessage", message);

            return CreatedAtAction(nameof(Get), new { id = messageId }, message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string messageId)
        {
            if (messageService.IsSentBy(messageId, User.Id()) == false)
                return NotFound();

            messageService.Delete(messageId);

            return NoContent();
        }
    }
}
