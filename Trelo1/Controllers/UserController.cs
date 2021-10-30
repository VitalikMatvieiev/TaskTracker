using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Trelo1.Interfaces;
using TreloBLL.DtoModel;
using TreloBLL.Interfaces;

namespace Trelo1.Controllers
{
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public UserController(IUserService userService, IFileService fileService)
        {
            _userService = userService;
            _fileService = fileService;
        }

        [HttpGet]
        [Route("api/users/")]
        [Authorize(Roles = "Admin")]
        public IList<UserDto> GetAllUsers()
        {
            IList<UserDto> userDtos = _userService.GetAllUsers();
            return userDtos;
        }

        [HttpPost]
        [Route("api/users/")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            await _userService.Create(user);
            return Ok();
        }
        [HttpDelete]
        [Route("api/users/")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser([FromBody] SingleModel<int> id)
        {
            bool hasDeleted = await _userService.DeleteUser(id.Value);
            if(hasDeleted)
            {
                return Ok();
            }
            return NoContent();
        }

        [HttpPost]
        [Route("api/users/photos")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> AddUserAvatar(IFormFile formFile)
        {
            var myControllerName = ControllerContext.ActionDescriptor.ActionName;
            if (formFile?.Length > 0)
            {
                var userAvatar = _fileService.ConvertToByte64(formFile);
                var userEmail = User.Identity.Name;
                await _userService.AddUserAvatar(userEmail, userAvatar);
                return Ok();
            }
            else
            {
                return NotFound();
            }
            
        }

    }
}
