using System.Threading.Tasks;
using NChatterServer.Core.Models;
using System.Collections.Generic;
using System;

namespace NChatterServer.Core.Services
{
    public interface IGroupService
    {
        Task<Group> ReadGroup(int id);
        Task CreateGroup(Group group);
        Task UpdateGroup(Group group, Group groupToUpdate);
        Task DeleteGroup(Group group);
        Task<IEnumerable<Message>> GetGroupMessages(int id, DateTime fromTime, DateTime toTime);
        Task<Message> GetLastGroupMessage(int id);
    }
}