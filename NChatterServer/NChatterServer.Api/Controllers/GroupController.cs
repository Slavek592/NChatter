using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<IEnumerable<GroupModel>>> GetAll(int idUser)
        {
            try
            {
                User test = await _userService.ReadUser(idUser);
                if (test == null)
                {
                    return NotFound("This user does not exist.");
                }
                IEnumerable<Group> groups = await _groupService.GetUserGroups(idUser);
                if (!groups.Any())
                {
                    return Ok();
                }
                List<GroupModel> groupModels = new List<GroupModel>();
                foreach (var group in groups)
                {
                    groupModels.Add(Mapping.GroupToModel(group));
                }
                return Ok(groupModels);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GroupModel>> Get(int idUser, int id)
        {
            try
            {
                User test = await _userService.ReadUser(idUser);
                if (test == null)
                {
                    return NotFound("This user does not exist.");
                }
                Group group = await _groupService.ReadGroup(id);
                if (group == null)
                {
                    return NotFound("This group does not exist.");
                }
                if (!Authorize.UserInGroup(await _groupService.GetGroupMembers(id), idUser))
                {
                    return StatusCode(405);
                }
                return Ok(Mapping.GroupToModel(group));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        [HttpPost("")]
        public async Task<ActionResult> Post(int idUser, Group group)
        {
            try
            {
                User test = await _userService.ReadUser(idUser);
                if (test == null)
                {
                    return NotFound("This user does not exist.");
                }
                if (group.Name == null)
                {
                    return BadRequest("Group must have name.");
                }
                List<User> users = new List<User>();
                try
                {
                    foreach (var user in group.Members)
                    {
                        users.Add(await _userService.ReadUser(user.Id));
                    }
                    users.Add(await _userService.ReadUser(idUser));
                }
                catch (Exception e)
                {
                    return BadRequest("Error in adding users to the group.");
                }

                group.Members = users;
                await _groupService.CreateGroup(group);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int idUser, int id, Group group)
        {
            try
            {
                User user = await _userService.ReadUser(idUser);
                if (user == null)
                {
                    return NotFound("This user does not exist.");
                }
                Group test = await _groupService.ReadGroup(id);
                if (test == null)
                {
                    return NotFound("This group does not exist.");
                }
                if (!Authorize.UserInGroup(await _groupService.GetGroupMembers(id), idUser))
                {
                    return StatusCode(405);
                }
                await _groupService.UpdateGroup(group, id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int idUser, int id)
        {
            try
            {
                User user = await _userService.ReadUser(idUser);
                if (user == null)
                {
                    return NotFound("This user does not exist.");
                }
                Group test = await _groupService.ReadGroup(id);
                if (test == null)
                {
                    return NotFound("This group does not exist.");
                }
                if (!Authorize.UserInGroup(await _groupService.GetGroupMembers(id), idUser))
                {
                    return StatusCode(405);
                }
                await _groupService.DeleteGroup(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}/Members")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetMembers(int idUser, int id)
        {
            try
            {
                User test = await _userService.ReadUser(idUser);
                if (test == null)
                {
                    return NotFound("This user does not exist.");
                }
                Group group = await _groupService.ReadGroup(id);
                if (group == null)
                {
                    return NotFound("This group does not exist.");
                }
                
                IEnumerable<User> users = await _groupService.GetGroupMembers(id);
                if (!Authorize.UserInGroup(users, idUser))
                {
                    return StatusCode(405);
                }
                List<UserModel> userModels = new List<UserModel>();
                foreach (var user in users)
                {
                    userModels.Add(Mapping.UserToModel(user));
                }
                return Ok(userModels);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}