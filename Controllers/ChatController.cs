using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using chatServer.Models;

namespace chatServer.Controllers
{
    [ApiController]
    [Route("chat")]
    public class ChatController : ControllerBase
    {
        private IHubContext<SignalR> _hub;
        private readonly ILogger<ChatController> _logger;
        private List<Room> RoomList = new List<Room>();
        public ChatController(ILogger<ChatController> logger, IHubContext<SignalR> hub)
        {
            _logger = logger;
            _hub = hub;
            RoomList.Add(new Room
            {
                Name = "General",
                Messages = new List<Messages>()
            });
        }

        [Route("SendMessages")]
        [HttpPost]
        public async Task<IActionResult> SendMessages([FromBody] Room model)
        {
            try
            {
                Room room = RoomList.Find(c => c.Name == model.Name);
                model.Messages[0].Date = DateTime.Now;
                room.Messages.Add(model.Messages[0]);
                await _hub.Clients.All.SendAsync("sendmessages", model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("LoadData")]
        [HttpPost]
        public IActionResult LoadData([FromBody] string roomName)
        {
            try
            {
                Room room = RoomList.Find(c => c.Name == roomName);
                List<Messages> response = room.Messages.Where(s => s.Date > DateTime.Now.AddHours(-1)).ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
