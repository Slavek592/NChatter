using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NChatterServer.Core.Models;
using NChatterServer.Data.Models;
using NChatterServer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using NChatterServer.Data;

namespace NChatterServer.Api.Controllers
{
    [ApiController]
    [Route("User/{idUser:int}/Groups")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;
        public GroupController(IGroupService groupService, IUserService userService)
        {
            _groupService = groupService;
            _userService = userService;
        }
        
        [HttpGet("")]
        public async Task<IEnumerable<GroupModel>> GetAll(int idUser)
        {
            IEnumerable<Group> groups = await _groupService.GetUserGroups(idUser);
            List<GroupModel> groupModels = new List<GroupModel>();
            foreach (var group in groups)
            {
                groupModels.Add(Mapping.GroupToModel(group));
            }
            return groupModels;
        }

        [HttpGet("{id:int}")]
        public async Task<GroupModel> Get(int idUser, int id)
        {
            return Mapping.GroupToModel(await _groupService.ReadGroup(id));
        }
        
        [HttpPost("")]
        public async Task Post(int idUser, Group group)
        {
            List<User> users = new List<User>();
            foreach (var user in group.Members)
            {
                users.Add(await _userService.ReadUser(user.Id));
            }

            group.Members = users;
            await _groupService.CreateGroup(group);
        }

        [HttpPut("{id:int}")]
        public async Task Put(int idUser, int id, Group group)
        {
            await _groupService.UpdateGroup(group, id);
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int idUser, int id)
        {
            await _groupService.DeleteGroup(id);
        }

        [HttpGet("{id:int}/Members")]
        public async Task<IEnumerable<UserModel>> GetMembers(int idUser, int id)
        {
            IEnumerable<User> users = await _groupService.GetGroupMembers(id);
            List<UserModel> userModels = new List<UserModel>();
            foreach (var user in users)
            {
                userModels.Add(Mapping.UserToModel(user));
            }

            return userModels;
        }
    }
}