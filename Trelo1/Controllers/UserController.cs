using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Trelo1.Interfaces;
using Trelo1.Models;

namespace Trelo1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IList<User> GetAllUsers()
        {
            IList<User> users = _userService.GetAllUsers();
            return users;
        }
        [HttpPost]
        [ActionName("CreateUser")]
        public IActionResult CreateUser([FromBody]User user)
        {
            if(user == null)
            {
                return BadRequest();
            }
            _userService.Create(user);
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return Ok();
        }

    }
}
