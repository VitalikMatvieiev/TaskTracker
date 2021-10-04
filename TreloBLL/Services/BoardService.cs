using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloDAL.Data;
using Trelo1.Interfaces;

using TreloDAL.UnitOfWork;
using TreloDAL.Models;
using TreloBLL.DtoModel;
using AutoMapper;

namespace Trelo1.Services
{
    public class BoardService : IBoardService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BoardService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void AddUserToBoard(int userId, int boardId)
        {
            if (userId != 0 && boardId != 0)
            {
                var user = _unitOfWork.Users.FirstOrDefault(u => u.Id == userId);
                var board = _unitOfWork.Boards.FirstOrDefault(b => b.Id == boardId);

                if (board != null)
                {
                    board.Users.Add(user);
                }
                _unitOfWork.SaveChanges();
            }
        }

        public void CreateBoard(BoardDto boardDto)
        {
            if(boardDto != null)
            {
                var board = _mapper.Map<Board>(boardDto);
                _unitOfWork.Boards.Create(board);
                _unitOfWork.SaveChanges();
            }
        }

        public bool DeleteBoard(int boardId)
        {
            if (boardId != 0)
            {
                var board = _unitOfWork.Boards.FirstOrDefault(b => b.Id == boardId);
                if(board != null)
                {
                    _unitOfWork.Boards.Remove(board);
                    _unitOfWork.SaveChanges();
                    return true;
                }  
            }

            return false;
        }

        public bool DeleteUserFromBoard(int userId, int boardId)
        {
            if (userId != 0 && boardId != 0)
            {
                var user = _unitOfWork.Users.FirstOrDefault(u => u.Id == userId);
                var board = _unitOfWork.Boards.FirstOrDefault(b => b.Id == boardId, includeProperties: "Users");

                if (board != null)
                {
                    board.Users.Remove(user);
                    _unitOfWork.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public List<BoardDto> GetBoards()
        {
            var boards = _unitOfWork.Boards.GetAll(includeProperties: "UserTasks,Organization");
            var boardDto = _mapper.Map<List<BoardDto>>(boards);
            return boardDto;
        }
    }
}
