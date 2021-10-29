using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trelo1.Interfaces;
using TreloBLL.DtoModel;
using TreloBLL.Interfaces;

namespace Trelo1.Controllers
{
    [ApiController]
    [Authorize]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;
        private readonly IAppAuthentication _appAuthentication;
        public BoardController(IBoardService boardService, IAppAuthentication appAuthentication)
        {
            _boardService = boardService;
            _appAuthentication = appAuthentication;
        }

        [HttpGet]
        [Route("api/boards/")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<BoardDto> GetAllBoards()
        {
            return _boardService.GetBoards();
        }

        [HttpPost]
        [Route("api/boards/")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBoard(BoardDto board)
        {
            if (board != null)
            {
                await _boardService.CreateBoard(board);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("api/boards/{boardId}/users/")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUserToBoard(int boardId, SingleModel<int> userId)
        {
            await _boardService.AddUserToBoard(userId.Value, boardId);
            return Ok();
        }

        [HttpDelete]
        [Route("api/boards/{boardId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBoard(int boardId)
        {
            bool hasDeleted = await _boardService.DeleteBoard(boardId);
            if (hasDeleted)
            {
                return Ok();
            }
            return NoContent();
        }
        [HttpDelete]
        [Route("api/boards/{boardId}/users/")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserFromBoard(int boardId, SingleModel<int> userId)
        {
            bool hasDeleted = await _boardService.DeleteUserFromBoard(userId.Value, boardId);
            if (hasDeleted)
            {
                return Ok();
            }

            return NoContent();
            
        }
    }
}
