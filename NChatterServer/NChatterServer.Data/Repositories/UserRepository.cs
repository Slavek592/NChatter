using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NChatterServer.Core.Repositories;
using NChatterServer.Core.Models;

namespace NChatterServer.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(NChatterServerContext context) : base(context) {}

        public async Task<IEnumerable<User>> FindByUsernameAsync(string username)
        {
            return await NChatterServerContext.Users
                .Where(u => u.Username == username)
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
        
        public async Task<User> GetWithGroupsByIdAsync(int id)
        {
            return await NChatterServerContext.Users
                .Where(u => u.Id == id)
                .Include(u => u.Groups)
                .FirstOrDefaultAsync();
        }
        public async Task<User> GetWithContactsByIdAsync(int id)
        {
            return await NChatterServerContext.Users
                .Where(u => u.Id == id)
                .Include(u => u.Contacts)
                .FirstOrDefaultAsync();
        }

        private NChatterServerContext NChatterServerContext
        {
            get { return Context as NChatterServerContext; }
        }
    }
}