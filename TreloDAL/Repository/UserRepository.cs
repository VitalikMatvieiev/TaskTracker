using System;
using System.Collections.Generic;
using System.Text;
using Trelo1.Data;
using Trelo1.Models;

namespace TreloDAL.Repository
{
    public class UserRepository : Repository<User>
    {
        private readonly TreloDbContext _db;
        public UserRepository(TreloDbContext db) : base(db)
        {
             
        }
    }
}
