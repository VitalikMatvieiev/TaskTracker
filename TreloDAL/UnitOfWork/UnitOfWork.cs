using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Trelo1.Data;
using TreloDAL.Repository;
using TreloDAL.Repository.IRepository;

namespace TreloDAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TreloDbContext _db;

        private BoardRepository boardRepository;
        private OrganizationRepository organizationRepository;
        private UserTaskRepository userTaskRepository;
        private UserRepository userRepository;
        public UnitOfWork(TreloDbContext db)
        {
            _db = db;
        }

        public BoardRepository Boards
        {
            get
            {
                if (boardRepository == null)
                    boardRepository = new BoardRepository(_db);
                return boardRepository;
            }
        }
        public OrganizationRepository Organizations
        {
            get
            {
                if (organizationRepository == null)
                    organizationRepository = new OrganizationRepository(_db);
                return organizationRepository;
            }
        }
        public UserTaskRepository UserTasks
        {
            get
            {
                if (userTaskRepository == null)
                    userTaskRepository = new UserTaskRepository(_db);
                return userTaskRepository;
            }
        }
        public UserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(_db);
                return userRepository;
            }
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

    }
}
