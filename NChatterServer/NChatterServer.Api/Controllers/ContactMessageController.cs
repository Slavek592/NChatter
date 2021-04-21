using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NChatterServer.Core.Models;
using NChatterServer.Data.Models;
using NChatterServer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using NChatterServer.Data;

namespace NChatterServer.Api.Controllers
{
    [ApiController]
    [Route("User/{idUser:int}/Contacts/{idContact:int}/Messages")]
    public class ContactMessageController
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
        public async Task<MessageModel> Get(int idUser, int idContact, int id)
        {
            Message message = await _messageService.ReadMessage(id);
            if (message.Status == MessageStatus.Status.Sent)
            {
                message.Status = MessageStatus.Status.Delivered;
                await _messageService.UpdateMessage(message, id);
            }
            return Mapping.MessageToModel(message);
        }

        [HttpGet("")]
        public async Task<IEnumerable<MessageModel>> GetAll(int idUser, int idContact, DateTime from, DateTime to)
        {
            IEnumerable<Message> messages = await _messageService.GetContactMessages(idContact, from, to);
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

            return messageModels;
        }

        [HttpPost("")]
        public async Task Post(int idUser, int idContact, Message message)
        {
            message.Contact = await _contactService.ReadContact(idContact);
            message.Status = MessageStatus.Status.Sent;
            await _messageService.CreateMessage(message);
        }

        [HttpPut("{id:int}")]
        public async Task Put(int idUser, int idContact, int id, Message message)
        {
            await _messageService.UpdateMessage(message, id);
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int idUser, int idContact, int id)
        {
            await _messageService.DeleteMessage(id);
        }
    }
}