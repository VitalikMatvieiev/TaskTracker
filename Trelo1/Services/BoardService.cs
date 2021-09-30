using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trelo1.Data;
using Trelo1.Interfaces;
using Trelo1.Models;

namespace Trelo1.Services
{
    public class BoardService : IBoardService
    {
        private readonly TreloDbContext _db;
        public BoardService(TreloDbContext db)
        {
            _db = db;
        }
        public void AddUserToBoard(int userId, int boardId)
        {
            if (userId != 0 && boardId != 0)
            {
                var user = _db.Users.FirstOrDefault(u => u.Id == userId);
                var board = _db.Boards.FirstOrDefault(b => b.Id == boardId);

                if (board != null)
                {
                    board.Users.Add(user);
                }
                _db.SaveChanges();
            }
        }

        public void CreateBoard(Board board)
        {
            if(board != null)
            {
                _db.Boards.Add(board);
                _db.SaveChanges();
            }
        }

        public void DeleteBoard(int boardId)
        {
            if (boardId != 0)
            {
                var board = _db.Boards.FirstOrDefault(b => b.Id == boardId);
                _db.Boards.Remove(board);
                _db.SaveChanges();
            }
        }

        public void DeleteUserFromBoard(int userId, int boardId)
        {
            if (userId != 0 && boardId != 0)
            {
                var user = _db.Users.FirstOrDefault(u => u.Id == userId);
                var board = _db.Boards.Include(b=>b.Users).FirstOrDefault(b => b.Id == boardId);

                if (board != null)
                {
                    board.Users.Remove(user);
                }
                _db.SaveChanges();
            }
        }
    }
}
