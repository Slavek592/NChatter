using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NChatterServer.Core.Services;
using NChatterServer.Core.Models;
using NChatterServer.Data.Models;
using System.Threading.Tasks;
using NChatterServer.Data;

namespace NChatterServer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            try
            {
                User user = await _userService.ReadUser(id);
                if (user == null)
                {
                    return NotFound("This user does not exist.");
                }
                return Ok(Mapping.UserToModel(user));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetByName(string name)
        {
            try
            {
                IEnumerable<User> users = await _userService.FindByUsername(name);
                if (users == null)
                {
                    return NotFound("No user with this username.");
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

        [HttpPost()]
        public async Task<ActionResult> Post(User user)
        {
            try
            {
                if (user.UserName == null | user.Password == null)
                {
                    return BadRequest("User must have username and password.");
                }
                await _userService.CreateUser(user);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, User user)
        {
            try
            {
                User test = await _userService.ReadUser(id);
                if (test == null)
                {
                    return NotFound("This user does not exist.");
                }
                if (user.UserName == null & user.Password == null)
                {
                    return BadRequest("Change username or password.");
                }
                await _userService.UpdateUser(id, user);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                User test = await _userService.ReadUser(id);
                if (test == null)
                {
                    return NotFound("This user does not exist.");
                }
                await _userService.DeleteUser(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}