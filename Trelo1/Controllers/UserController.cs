﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Trelo1.Interfaces;
using TreloBLL.DtoModel;


namespace Trelo1.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("api/users/")]
        public IList<UserDto> GetAllUsers()
        {
            IList<UserDto> userDtos = _userService.GetAllUsers();
            return userDtos;
        }

        [HttpPost]
        [Route("api/users/")]
        public IActionResult CreateUser([FromBody] UserDto user)
        {
            if(user == null)
            {
                return BadRequest();
            }
            _userService.Create(user);
            return Ok();
        }
        [HttpDelete]
        [Route("api/users/")]
        public async Task<IActionResult> DeleteUser(SingleModel<int> id)
        {
            bool hasDeleted = await _userService.DeleteUser(id.Value);
            if(hasDeleted)
            {
                return Ok();
            }
            return NoContent();
           
        }

    }
}
