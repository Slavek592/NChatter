using System.Collections.Generic;
using System.Threading.Tasks;
using NChatterServer.Data;
using NChatterServer.Data.Models;

namespace NChatterServer.Services
{
    public class MessageService
    {
        private readonly UnitOfWork _unitOfWork;

        public MessageService(UnitOfWork unitOfWork)
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

        public async Task UpdateMessage(Message message, Message messageToUpdate)
        {
            messageToUpdate.Text = message.Text;
            messageToUpdate.SentTime = message.SentTime;
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteMessage(Message message)
        {
            _unitOfWork.Messages.Remove(message);
            await _unitOfWork.CommitAsync();
        }
    }
}