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
        public async Task<UserModel> Get(int id)
        {
            return Mapping.UserToModel(await _userService.ReadUser(id));
        }

        [HttpGet("{name}")]
        public async Task<IEnumerable<UserModel>> GetByName(string name)
        {
            IEnumerable<User> users = await _userService.FindByUsername(name);
            List<UserModel> userModels = new List<UserModel>();
            foreach (var user in users)
            {
                userModels.Add(Mapping.UserToModel(user));
            }
            return userModels;
        }

        [HttpPost()]
        public async Task Post(User user)
        {
            await _userService.CreateUser(user);
        }

        [HttpPut("{id:int}")]
        public async Task Put(int id, User user)
        {
            await _userService.UpdateUser(id, user);
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id)
        {
            await _userService.DeleteUser(id);
        }
    }
}