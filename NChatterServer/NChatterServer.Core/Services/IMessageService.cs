using NChatterServer.Core.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace NChatterServer.Core.Services
{
    public interface IMessageService
    {
        Task<Message> ReadMessage(int id);
        Task CreateMessage(Message message);
        Task UpdateMessage(Message message, int id);
        Task DeleteMessage(int id);
        Task<IEnumerable<Message>> GetGroupMessages(int id, DateTime fromTime, DateTime toTime);
        Task<IEnumerable<Message>> GetContactMessages(int id, DateTime fromTime, DateTime toTime);
        Task<Message> GetLastGroupMessage(int id);
    }
}