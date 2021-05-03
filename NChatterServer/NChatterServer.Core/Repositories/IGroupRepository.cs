using System.Threading.Tasks;
using System.Collections.Generic;
using NChatterServer.Core.Models;
using System;

namespace NChatterServer.Core.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<IEnumerable<Message>> GetMessagesByGroupIdAndTimeAsync(int id, DateTime time1, DateTime time2);
        Task<Message> GetLastMessageByGroupAsync(int id);
        Task<Group> GetWithMembersByIdAsync(int id);
    }
}