using System;
using System.Collections.Generic;
using System.Text;
using Trelo1.Data;
using Trelo1.Models;

namespace TreloDAL.Repository
{
    public class UserTaskRepository : Repository<UserTask>
    {
        private readonly TreloDbContext _db;
        public UserTaskRepository(TreloDbContext db) : base(db)
        {

        }
    }
}
