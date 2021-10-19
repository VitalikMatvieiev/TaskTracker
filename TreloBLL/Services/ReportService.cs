using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreloBLL.Interfaces;
using TreloDAL.Data;

namespace TreloBLL.Services
{
    public class ReportService : IReportService
    {
        private readonly TreloDbContext _dbContext;
        private readonly IMapper _mapper;

        public ReportService(TreloDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public string GenereteBoardTasksReport(int boardId)
        {
            var boardTasks = _dbContext.Tasks.Where(u => u.BoardId == boardId);

            var builder = new StringBuilder();
            builder.AppendLine("Id, Name, Decription, CreatedDate, DueDate");

            foreach (var task in boardTasks)
            {
                builder.AppendLine($"{task.Id},{task.Name},{task.Description},{task.CreatedDate},{task.DueDate}");
            }

            return builder.ToString();
            
        }

        public string GenereteUserTasksReport(int userId)
        {
            var userTasks = _dbContext.Tasks.Where(u => u.AssignedUserId == userId);

            var builder = new StringBuilder();
            builder.AppendLine("Id, Name, Decription, CreatedDate, DueDate");

            foreach (var task in userTasks)
            {
                builder.AppendLine($"{task.Id},{task.Name},{task.Description},{task.CreatedDate},{task.DueDate}");
            }

            return builder.ToString();
        }
    }
}
