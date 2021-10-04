using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloBLL.DtoModel;


namespace Trelo1.Interfaces
{
    public interface IBoardService
    {
        void CreateBoard(BoardDto board);
        bool DeleteBoard(int boardId);
        void AddUserToBoard(int userId, int boardId);
        bool DeleteUserFromBoard(int userId, int boardId);
        List<BoardDto> GetBoards();
    }
}
