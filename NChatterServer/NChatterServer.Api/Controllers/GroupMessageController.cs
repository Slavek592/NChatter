using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using NChatterServer.Core.Models;
using NChatterServer.Data.Models;
using NChatterServer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using NChatterServer.Data;

namespace NChatterServer.Api.Controllers
{
    [ApiController]
    [Route("User/{idUser:int}/Groups/{idGroup:int}/Messages")]
    public class GroupMessageController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        public GroupMessageController(IGroupService groupService, IUserService userService, IMessageService messageService)
        {
            _groupService = groupService;
            _userService = userService;
            _messageService = messageService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MessageModel>> Get(int idUser, int idGroup, int id)
        {
            try
            {
                Group test = await _groupService.ReadGroup(idGroup);
                if (test == null)
                {
                    return NotFound("This group does not exist.");
                }
                IEnumerable<User> members = await _groupService.GetGroupMembers(idGroup);
                User user = await _userService.ReadUser(idUser);
                if (user == null)
                {
                    return NotFound("This user does not exist.");
                }

                if (!Authorize.UserInGroup(members, idUser))
                {
                    return StatusCode(405);
                }
                Message message = await _messageService.ReadMessage(id);
                if (message == null)
                {
                    return NotFound("This message does not exist.");
                }
                else if (message.Status == MessageStatus.Status.Sent)
                {
                    message.Status = MessageStatus.Status.Delivered;
                    await _messageService.UpdateMessage(message, id);
                }
                return Ok(Mapping.MessageToModel(message));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<MessageModel>>> GetAll(int idUser, int idGroup, DateTime from, DateTime to)
        {
            try
            {
                Group test = await _groupService.ReadGroup(idGroup);
                if (test == null)
                {
                    return NotFound("This group does not exist.");
                }
                IEnumerable<User> members = await _groupService.GetGroupMembers(idGroup);
                User user = await _userService.ReadUser(idUser);
                if (user == null)
                {
                    return NotFound("This user does not exist.");
                }
                if (!Authorize.UserInGroup(members, idUser))
                {
                    return StatusCode(405);
                }
                IEnumerable<Message> messages = await _messageService.GetGroupMessages(idGroup, from, to);
                if (!messages.Any())
                {
                    return Ok();
                }
                List<MessageModel> messageModels = new List<MessageModel>();
                foreach (var message in messages)
                {
                    if (message.Status == MessageStatus.Status.Sent)
                    {
                        message.Status = MessageStatus.Status.Delivered;
                        await _messageService.UpdateMessage(message, message.Id);
                    }
                    messageModels.Add(Mapping.MessageToModel(message));
                }
                return Ok(messageModels);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("")]
        public async Task<ActionResult> Post(int idUser, int idGroup, Message message)
        {
            try
            {
                Group group = await _groupService.ReadGroup(idGroup);
                if (group == null)
                {
                    return NotFound("This group does not exist.");
                }
                IEnumerable<User> members = await _groupService.GetGroupMembers(idGroup);
                User user = await _userService.ReadUser(idUser);
                if (user == null)
                {
                    return NotFound("This user does not exist.");
                }
                if (!Authorize.UserInGroup(members, idUser))
                {
                    return StatusCode(405);
                }

                if (message.Text == null)
                {
                    return BadRequest("Message must have some text.");
                }
                
                message.SentTime = DateTime.Now;
                message.Group = group;
                message.Status = MessageStatus.Status.Sent;
                message.SentFrom = idUser;
                await _messageService.CreateMessage(message);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int idUser, int idGroup, int id, Message message)
        {
            try
            {
                Group group = await _groupService.ReadGroup(idGroup);
                if (group == null)
                {
                    return NotFound("This group does not exist.");
                }
                IEnumerable<User> members = await _groupService.GetGroupMembers(idGroup);
                User user = await _userService.ReadUser(idUser);
                if (user == null)
                {
                    return NotFound("This user does not exist.");
                }
                if (!Authorize.UserInGroup(members, idUser))
                {
                    return StatusCode(405);
                }
                Message test = await _messageService.ReadMessage(id);
                if (test == null)
                {
                    return NotFound("This message does not exist.");
                }
                if (test.SentFrom != idUser)
                {
                    return StatusCode(405);
                }

                if (message.Text == null)
                {
                    return BadRequest("Message must have some text.");
                }
                
                await _messageService.UpdateMessage(message, id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int idUser, int idGroup, int id)
        {
            try
            {
                Group group = await _groupService.ReadGroup(idGroup);
                if (group == null)
                {
                    return NotFound("This group does not exist.");
                }
                IEnumerable<User> members = await _groupService.GetGroupMembers(idGroup);
                User user = await _userService.ReadUser(idUser);
                if (user == null)
                {
                    return NotFound("This user does not exist.");
                }
                if (!Authorize.UserInGroup(members, idUser))
                {
                    return StatusCode(405);
                }
                Message test = await _messageService.ReadMessage(id);
                if (test == null)
                {
                    return NotFound("This message does not exist.");
                }
                if (test.SentFrom != idUser)
                {
                    return StatusCode(405);
                }
                await _messageService.DeleteMessage(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}