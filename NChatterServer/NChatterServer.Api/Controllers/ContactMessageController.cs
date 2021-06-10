using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NChatterServer.Core.Models;
using NChatterServer.Data.Models;
using NChatterServer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using NChatterServer.Data;
using System.Linq;

namespace NChatterServer.Api.Controllers
{
    [ApiController]
    [Route("User/{idUser:int}/Contacts/{idContact:int}/Messages")]
    public class ContactMessageController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        public ContactMessageController(IContactService contactService, IUserService userService, IMessageService messageService)
        {
            _contactService = contactService;
            _userService = userService;
            _messageService = messageService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MessageModel>> Get(int idUser, int idContact, int id)
        {
            try
            {
                Contact test = await _contactService.ReadContact(idContact);
                if (test == null)
                {
                    return NotFound("This contact does not exist.");
                }
                IEnumerable<User> members = await _contactService.GetContactMembers(idContact);
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
                if (message.Status == MessageStatus.Status.Sent)
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
        public async Task<ActionResult<IEnumerable<MessageModel>>> GetAll(int idUser, int idContact)
        {
            try
            {
                DateTime from = DateTime.Today;
                DateTime to = DateTime.Now;
                Contact test = await _contactService.ReadContact(idContact);
                if (test == null)
                {
                    return NotFound("This contact does not exist.");
                }
                IEnumerable<User> members = await _contactService.GetContactMembers(idContact);
                User user = await _userService.ReadUser(idUser);
                if (user == null)
                {
                    return NotFound("This user does not exist.");
                }
                if (!Authorize.UserInGroup(members, idUser))
                {
                    return StatusCode(405);
                }
                IEnumerable<Message> response = await _messageService.GetContactMessages(idContact, from, to);
                List<Message> messages = response.ToList();
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
        public async Task<ActionResult> Post(int idUser, int idContact, MessageModel messageModel)
        {
            try
            {
                Message message = Mapping.ModelToMessage(messageModel);
                Contact contact = await _contactService.ReadContact(idContact);
                if (contact == null)
                {
                    return NotFound("This contact does not exist.");
                }
                IEnumerable<User> members = await _contactService.GetContactMembers(idContact);
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
                message.Contact = contact;
                message.SentFrom = idUser;
                message.Status = MessageStatus.Status.Sent;
                await _messageService.CreateMessage(message);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int idUser, int idContact, int id, MessageModel messageModel)
        {
            try
            {
                Message message = Mapping.ModelToMessage(messageModel);
                message.Status = MessageStatus.Status.Sent;
                message.SentTime = DateTime.Now;
                Contact contact = await _contactService.ReadContact(idContact);
                if (contact == null)
                {
                    return NotFound("This contact does not exist.");
                }
                IEnumerable<User> members = await _contactService.GetContactMembers(idContact);
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
        public async Task<ActionResult> Delete(int idUser, int idContact, int id)
        {
            try
            {
                Contact contact = await _contactService.ReadContact(idContact);
                if (contact == null)
                {
                    return NotFound("This contact does not exist.");
                }
                IEnumerable<User> members = await _contactService.GetContactMembers(idContact);
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