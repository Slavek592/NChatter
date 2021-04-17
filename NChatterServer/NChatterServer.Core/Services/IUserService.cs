using NChatterServer.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NChatterServer.Core.Services
{
    public interface IUserService
    {
        Task CreateUser(User user);
        Task DeleteUser(User user);
        Task<User> ReadUser(int id);
        Task<IEnumerable<User>> FindByUsername(string username);
        Task UpdateUser(User userToBeUpdated, User user);
        Task<IEnumerable<Group>> GetGroups(int id);
        Task<IEnumerable<Contact>> GetContacts(int id);
    }
}