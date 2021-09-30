using System;
using System.Collections.Generic;
using System.Text;
using Trelo1.Data;
using Trelo1.Models;

namespace TreloDAL.Repository
{
    public class OrganizationRepository : Repository<Organization>
    {
        private readonly TreloDbContext _db;
        public OrganizationRepository(TreloDbContext db) : base(db)
        {

        }
    }
}
