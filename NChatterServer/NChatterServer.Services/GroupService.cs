using System;
using System.Collections.Generic;
using NChatterServer.Data;
using System.Threading.Tasks;
using NChatterServer.Core.Models;
using NChatterServer.Core.Services;

namespace NChatterServer.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        
        public async Task<Group> ReadGroup(int id)
        {
            return await _unitOfWork.Groups.GetByIdAsync(id);
        }

        public async Task CreateGroup(Group group)
        {
            await _unitOfWork.Groups.AddAsync(group);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateGroup(Group group, int id)
        {
            Group groupToUpdate = await _unitOfWork.Groups.GetByIdAsync(id);
            groupToUpdate.Name = group.Name;
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteGroup(int id)
        {
            Group group = await _unitOfWork.Groups.GetByIdAsync(id);
            _unitOfWork.Groups.Remove(group);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Group>> GetUserGroups(int id)
        {
            User user = await _unitOfWork.Users.GetWithGroupsByIdAsync(id);

            return user.Groups;
        }

        public async Task<IEnumerable<User>> GetGroupMembers(int id)
        {
            Group group = await _unitOfWork.Groups.GetWithMembersByIdAsync(id);
            return group.Members;
        }
    }
}