using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NChatterServer.Data.Models;

namespace NChatterServer.Data.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(NChatterServerContext context) : base(context) {}

        public async Task<IEnumerable<User>> FindByUsernameAsync(string username)
        {
            return await NChatterServerContext.Users
                .Include(u => u.UserName == username)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Contact>> GetContactsByUserIdAsync(int id)
        {
            User user = await GetByIdAsync(id);
            return from contact in user.Contacts
                select contact;
        }
        
        public async Task<IEnumerable<Group>> GetGroupsByUserIdAsync(int id)
        {
            User user = await GetByIdAsync(id);
            return from g in user.Groups
                select g;
        }

        private NChatterServerContext NChatterServerContext
        {
            get { return Context as NChatterServerContext; }
        }
    }
}