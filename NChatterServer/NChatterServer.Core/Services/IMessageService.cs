using NChatterServer.Core.Models;
using System.Threading.Tasks;

namespace NChatterServer.Core.Services
{
    public interface IMessageService
    {
        Task<Message> ReadMessage(int id);
        Task CreateMessage(Message message);
        Task UpdateMessage(Message message, Message messageToUpdate);
        Task DeleteMessage(Message message);
    }
}