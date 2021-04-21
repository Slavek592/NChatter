using NChatterServer.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NChatterServer.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> FindByUsernameAsync(string username);
        Task<IEnumerable<Contact>> GetContactsByUserIdAsync(int id);
        Task<IEnumerable<Group>> GetGroupsByUserIdAsync(int id);
        Task<User> GetWithGroupsByIdAsync(int id);
        Task<User> GetWithContactsByIdAsync(int id);
    }
}