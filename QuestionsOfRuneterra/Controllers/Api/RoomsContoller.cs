using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QuestionsOfRuneterra.Data.Models;
using QuestionsOfRuneterra.Hubs;
using QuestionsOfRuneterra.Infrastructure.Extensions;
using QuestionsOfRuneterra.Models.Rooms;
using QuestionsOfRuneterra.Services.Rooms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsOfRuneterra.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService roomService;

        private readonly IHubContext<ChatHub> hubContext;

        public RoomsController(IHubContext<ChatHub> hubContext, IRoomService roomService)
        {
            this.hubContext = hubContext;
            this.roomService = roomService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RoomServiceModel>> Get()
        {
            return Ok(roomService.Rooms(new bool[] { true }));
        }

        [HttpGet("{id}")]
        public ActionResult<Room> Get(string roomId)
        {
            if (roomService.Exists(roomId) == false)
                return NotFound();

            return Ok(roomService.Room(roomId));
        }

        [HttpPost]
        public ActionResult<Room> Create(RoomServiceModel room)
        {
            if (roomService.IsNameUnique(room.Name))
                return BadRequest("Invalid room name or room already exists");

            room = roomService.Room(roomService.Add(room.Name, User.Id()));

            hubContext.Clients.All.SendAsync("addChatRoom", new { id = room.Id, name = room.Name });

            return CreatedAtAction(nameof(Get), new { id = room.Id }, new { id = room.Id, name = room.Name });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, RoomServiceModel room)
        {
            if (roomService.IsNameUnique(room.Name))
                return BadRequest("Invalid room name or room already exists");

            if (roomService.IsOwnedBy(room.Id, User.Id()))
                return BadRequest();

            roomService.Edit(room.Id, room.Name);

            hubContext.Clients.All.SendAsync("updateChatRoom", new { id = room.Id, room.Name });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string roomId)
        {
            if (roomService.IsOwnedBy(roomId, User.Id()))
                return BadRequest();

            if (roomService.Exists(roomId))
                return BadRequest();

            var room = roomService.Delete(roomId);

            hubContext.Clients.All.SendAsync("removeChatRoom", room.Id);
            hubContext.Clients.Group(room.Name).SendAsync("onRoomDeleted", string.Format("Room {0} has been deleted.\nYou are moved to the first available room!", room.Name));

            return NoContent();
        }
    }
}
