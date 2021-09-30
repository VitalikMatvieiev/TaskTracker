using System;
using System.Collections.Generic;
using System.Text;
using TreloDAL.Repository.IRepository;

namespace TreloDAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public void Save();
    }
}
