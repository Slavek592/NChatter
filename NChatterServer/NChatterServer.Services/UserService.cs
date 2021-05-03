using System.Collections.Generic;
using System.Threading.Tasks;
using NChatterServer.Data;
using NChatterServer.Core.Models;
using NChatterServer.Core.Services;

namespace NChatterServer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task CreateUser(User user)
        {
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteUser(int id)
        {
            User user = await _unitOfWork.Users.GetByIdAsync(id);
            _unitOfWork.Users.Remove(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task<User> ReadUser(int id)
        {
            return await _unitOfWork.Users.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> FindByUsername(string username)
        {
            return await _unitOfWork.Users.FindByUsernameAsync(username);
        }

        public async Task UpdateUser(int id, User user)
        {
            User userToBeUpdated = await _unitOfWork.Users.GetByIdAsync(id);

            if (user.UserName != null)
            {
                userToBeUpdated.UserName = user.UserName;
            }
            if (user.Password != null)
            {
                userToBeUpdated.Password = user.Password;
            }

            await _unitOfWork.CommitAsync();
        }
    }
}