using System;
using NChatterServer.Core.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NChatterServer.Core.Repositories;

namespace NChatterServer.Data.Repositories
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(NChatterServerContext context) : base(context) {}

        public async Task<IEnumerable<Message>> GetMessagesByGroupIdAndTimeAsync(int id, DateTime time1, DateTime time2)
        {
            Group g = await GetByIdAsync(id);
            return from m in g.Messages
                where time1 < m.SentTime & m.SentTime <= time2
                select m;
        }
        
        public async Task<Message> GetLastMessageByGroupAsync(int id)
        {
            Group g = await GetByIdAsync(id);
            return g.Messages.OrderByDescending(m => m.SentTime).First();
        }

        public async Task<Group> GetWithMembersByIdAsync(int id)
        {
            return await NChatterServerContext.Groups
                .Where(g => g.Id == id)
                .Include(g => g.Members)
                .FirstOrDefaultAsync();
        }

        private NChatterServerContext NChatterServerContext
        {
            get { return Context as NChatterServerContext; }
        }
    }
}