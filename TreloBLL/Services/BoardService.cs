using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trelo1.Data;
using Trelo1.Interfaces;
using Trelo1.Models;
using TreloDAL.UnitOfWork;

namespace Trelo1.Services
{
    public class BoardService : IBoardService
    {
        private readonly UnitOfWork _unitOfWork;
        public BoardService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public void CreateBoard(Board board)
        {
            if(board != null)
            {
                _unitOfWork.Boards.Create(board);
                _unitOfWork.SaveChanges();
            }
        }

        public void DeleteBoard(int boardId)
        {
            if (boardId != 0)
            {
                var board = _unitOfWork.Boards.FirstOrDefault(b => b.Id == boardId);
                _unitOfWork.Boards.Remove(board);
                _unitOfWork.SaveChanges();
            }
        }

        public void DeleteUserFromBoard(int userId, int boardId)
        {
            if (userId != 0 && boardId != 0)
            {
                var user = _unitOfWork.Users.FirstOrDefault(u => u.Id == userId);
                var board = _unitOfWork.Boards.FirstOrDefault(b => b.Id == boardId, includeProperties: "Users");

                if (board != null)
                {
                    board.Users.Remove(user);
                }
                _unitOfWork.SaveChanges();
            }
        }
    }
}
