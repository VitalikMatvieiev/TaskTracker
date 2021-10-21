using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Trelo1.Interfaces;
using TreloDAL.Models;
using TreloBLL.DtoModel;
using AutoMapper;
using TreloDAL.Data;
using System.Threading.Tasks;

namespace Trelo1.Services
{
    public class BoardService : IBoardService
    {
        private readonly TreloDbContext _dbContext;
        private readonly IMapper _mapper;

        public BoardService(TreloDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddUserToBoard(int userId, int boardId)
        {
            if (userId != 0 && boardId != 0)
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
                var board = await _dbContext.Boards.FirstOrDefaultAsync(b => b.Id == boardId);

                if (board != null)
                {
                    board.Users.Add(user);
                }
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CreateBoard(BoardDto boardDto)
        {
            if(boardDto != null)
            {
                var board = _mapper.Map<Board>(boardDto);
                _dbContext.Boards.Add(board);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteBoard(int boardId)
        {
            if (boardId != 0)
            {
                var board = await _dbContext.Boards.FirstOrDefaultAsync(b => b.Id == boardId);
                if(board != null)
                {
                    _dbContext.Boards.Remove(board);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }  
            }

            return false;
        }

        public async Task<bool> DeleteUserFromBoard(int userId, int boardId)
        {
            if (userId != 0 && boardId != 0)
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
                var board = await _dbContext.Boards.Include(p=>p.Users).FirstOrDefaultAsync(b => b.Id == boardId);

                if (board != null)
                {
                    board.Users.Remove(user);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
            }

            return false;
        }

        public List<BoardDto> GetBoards()
        {
            var boards = _dbContext.Boards.Include(p => p.UserTasks).Include(p => p.Organization);
            var boardDto = _mapper.Map<List<BoardDto>>(boards);
            return boardDto;
        }
    }
}
