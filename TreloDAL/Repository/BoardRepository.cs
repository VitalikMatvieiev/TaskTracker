using System;
using System.Collections.Generic;
using System.Text;
using TreloDAL.Data;
using TreloDAL.Models;

namespace TreloDAL.Repository
{
    public class BoardRepository : Repository<Board>
    {
        private readonly TreloDbContext _db;
        public BoardRepository(TreloDbContext db) : base(db)
        {
             
        }
    }
}
