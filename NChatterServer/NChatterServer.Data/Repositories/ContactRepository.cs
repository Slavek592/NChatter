using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NChatterServer.Core.Models;
using System.Linq;
using NChatterServer.Core.Repositories;

namespace NChatterServer.Data.Repositories
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(NChatterServerContext context) : base(context) {}
        
        public async Task<IEnumerable<Message>> GetMessagesByContactIdAndTimeAsync(int id, DateTime time1, DateTime time2)
        {
            Contact c = await GetByIdAsync(id);
            return from m in c.Messages
                where time1 < m.SentTime & m.SentTime <= time2
                select m;
        }
        
        public async Task<Message> GetLastMessageByContactAsync(int id)
        {
            Contact c = await GetByIdAsync(id);
            return c.Messages.OrderByDescending(m => m.SentTime).First();
        }
        public async Task<Contact> GetWithMembersByIdAsync(int id)
        {
            return await NChatterServerContext.Contacts
                .Where(c => c.Id == id)
                .Include(g => g.Users)
                .FirstOrDefaultAsync();
        }

        private NChatterServerContext NChatterServerContext
        {
            get { return Context as NChatterServerContext; }
        }
    }
}