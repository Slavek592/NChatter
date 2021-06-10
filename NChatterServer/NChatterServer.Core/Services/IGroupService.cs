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
        Task UpdateGroup(Group group, int id);
        Task DeleteGroup(int id);
        Task<IEnumerable<Group>> GetUserGroups(int id);
        Task<IEnumerable<User>> GetGroupMembers(int id);
    }
}