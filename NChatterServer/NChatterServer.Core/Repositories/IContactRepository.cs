using NChatterServer.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace NChatterServer.Core.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<IEnumerable<Message>> GetMessagesByContactIdAndTimeAsync(int id, DateTime time1,
            DateTime time2);
        Task<Message> GetLastMessageByContactAsync(int id);
    }
}