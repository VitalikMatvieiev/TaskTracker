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
    [Route("api/[controller]/[action]")]
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
        [Authorize(Roles = "Admin")]
        public IEnumerable<BoardDto> GetAllBoards()
        {
            return _boardService.GetBoards();
        }

        [HttpPost]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUserToBoard(int userId, int boardId)
        {
            await _boardService.AddUserToBoard(userId, boardId);
            return Ok();
        }

        [HttpDelete]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserFromBoard(int userId, int boardId)
        {
            bool hasDeleted = await _boardService.DeleteUserFromBoard(userId, boardId);
            if (hasDeleted)
            {
                return Ok();
            }

            return NoContent();
            
        }
    }
}
