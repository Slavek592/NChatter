using System.Collections.Generic;
using System.Threading.Tasks;
using NChatterServer.Data;
using NChatterServer.Core.Models;
using NChatterServer.Core.Services;
using System;
using System.Linq;

namespace NChatterServer.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Message> ReadMessage(int id)
        {
            return await _unitOfWork.Messages.GetByIdAsync(id);
        }

        public async Task CreateMessage(Message message)
        {
            await _unitOfWork.Messages.AddAsync(message);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateMessage(Message message, int id)
        {
            Message messageToUpdate = await _unitOfWork.Messages.GetByIdAsync(id);
            messageToUpdate.Text = message.Text;
            messageToUpdate.Status = message.Status;
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteMessage(int id)
        {
            Message message = await _unitOfWork.Messages.GetByIdAsync(id);
            _unitOfWork.Messages.Remove(message);
            await _unitOfWork.CommitAsync();
        }
        
        public async Task<IEnumerable<Message>> GetGroupMessages(int id, DateTime fromTime, DateTime toTime)
        {
            Group group = await _unitOfWork.Groups.GetMessagesByGroupIdAndTimeAsync(id, fromTime, toTime);
            return group.Messages.OrderBy(x => x.SentTime);
        }
        public async Task<IEnumerable<Message>> GetContactMessages(int id, DateTime fromTime, DateTime toTime)
        {
            Contact contact = await _unitOfWork.Contacts.GetMessagesByContactIdAndTimeAsync(id, fromTime, toTime);
            return contact.Messages.OrderBy(x => x.SentTime);
        }

        public async Task<Message> GetLastGroupMessage(int id)
        {
            return await _unitOfWork.Groups.GetLastMessageByGroupAsync(id);
        }
    }
}