using NChatterServer.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NChatterServer.Core.Services
{
    public interface IUserService
    {
        Task CreateUser(User user);
        Task DeleteUser(int id);
        Task<User> ReadUser(int id);
        Task<IEnumerable<User>> FindByUsername(string username);
        Task UpdateUser(int id, User user);
    }
}