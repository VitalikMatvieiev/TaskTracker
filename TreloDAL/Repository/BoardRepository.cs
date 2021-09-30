using System;
using System.Collections.Generic;
using System.Text;
using Trelo1.Data;
using Trelo1.Models;

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
