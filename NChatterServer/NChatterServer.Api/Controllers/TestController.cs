using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NChatterServer.Core.Models;
using NChatterServer.Core.Services;

namespace NChatterServer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IUserService _userService;
        public TestController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetSlava")]
        public async Task<IEnumerable<User>> GetSlava()
        {
            return await _userService.FindByUsername("Slava");
        }
        
        [HttpGet("")]
        public string GetSomething()
        {
            return "Here is something.";
        }
    }
}
