using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using QuestionsOfRuneterra.Hubs;
using QuestionsOfRuneterra.Infrastructure.Extensions;
using QuestionsOfRuneterra.Models.Uploads;
using QuestionsOfRuneterra.Services.Messages;
using QuestionsOfRuneterra.Services.Rooms;
using QuestionsOfRuneterra.Services.Uploads;

namespace QuestionsOfRuneterra.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService uploadService;
        private readonly IMessageService messageService;
        private readonly IRoomService roomService;
        private readonly IHubContext<ChatHub> hubContext;

        public UploadController(IMessageService messageService, IRoomService roomService, IUploadService uploadService, IHubContext<ChatHub> hubContext)
        {
            this.hubContext = hubContext;
            this.messageService = messageService;
            this.roomService = roomService;
            this.uploadService = uploadService;
            this.hubContext = hubContext;
        }

        [HttpPost]
        public IActionResult Upload([FromForm] UploadServiceModel upload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            if (uploadService.Validate(upload.File))
            {
                return BadRequest("Invalid file size");
            }

            var message = messageService.Message(messageService.Add(uploadService.Upload(upload.File), upload.ToRoomId, User.Id()));

            hubContext.Clients.Group(roomService.Name(upload.ToRoomId)).SendAsync("newMessage", message);

            return Ok();
        }
    }
}
