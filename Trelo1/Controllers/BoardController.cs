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
        public IActionResult CreateBoard(BoardDto board)
        {
            if (board != null)
            {
                _boardService.CreateBoard(board);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddUserToBoard(int userId, int boardId)
        {
            _boardService.AddUserToBoard(userId, boardId);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBoard(int boardId)
        {
            _boardService.DeleteBoard(boardId);
            return Ok();
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUserFromBoard(int userId, int boardId)
        {
            _boardService.DeleteUserFromBoard(userId, boardId);
            return Ok();
        }
    }
}
