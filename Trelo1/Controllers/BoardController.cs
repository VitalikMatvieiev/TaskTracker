﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trelo1.Interfaces;
using TreloBLL.DtoModel;

namespace Trelo1.Controllers
{
    [ApiController]
    [Authorize]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;
        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet]
        [Route("api/boards/")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<BoardDto> GetAllBoards()
        {
            return _boardService.GetBoards();
        }

        [HttpPost]
        [Route("api/board/")]
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
        [Route("api/board/{boardId}/add-user/")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUserToBoard(int boardId, SingleModel<int> userId)
        {
            await _boardService.AddUserToBoard(userId.Value, boardId);
            return Ok();
        }

        [HttpDelete]
        [Route("api/board/{boardId}")]
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
        [Route("api/board/{boardId}/user/")]
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
