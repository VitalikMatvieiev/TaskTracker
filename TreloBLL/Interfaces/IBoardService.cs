using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloBLL.DtoModel;


namespace Trelo1.Interfaces
{
    public interface IBoardService
    {
        Task CreateBoard(BoardDto board);
        Task<bool> DeleteBoard(int boardId);
        Task AddUserToBoard(int userId, int boardId);
        Task<bool> DeleteUserFromBoard(int userId, int boardId);
        List<BoardDto> GetBoards();
    }
}
