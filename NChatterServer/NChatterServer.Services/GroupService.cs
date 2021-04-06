using System;
using System.Collections.Generic;
using NChatterServer.Data;
using System.Threading.Tasks;
using NChatterServer.Data.Models;

namespace NChatterServer.Services
{
    public class GroupService
    {
        private readonly UnitOfWork _unitOfWork;

        public GroupService(UnitOfWork unitOfWork)
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

        public async Task UpdateGroup(Group group, Group groupToUpdate)
        {
            groupToUpdate.Name = group.Name;
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteGroup(Group group)
        {
            _unitOfWork.Groups.Remove(group);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Message>> GetGroupMessages(int id, DateTime fromTime, DateTime toTime)
        {
            return await _unitOfWork.Groups.GetMessagesByGroupIdAndTimeAsync(id, fromTime, toTime);
        }

        public async Task<Message> GetLastGroupMessage(int id)
        {
            return await _unitOfWork.Groups.GetLastMessageByGroupAsync(id);
        }
    }
}