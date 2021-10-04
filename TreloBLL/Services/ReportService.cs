using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TreloBLL.Interfaces;
using TreloDAL.UnitOfWork;

namespace TreloBLL.Services
{
    public class ReportService : IReportService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public string GenereteBoardTasksReport(int boardId)
        {
            var boardTasks = _unitOfWork.UserTasks.GetAll(u => u.BoardId == boardId);

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
            var userTasks = _unitOfWork.UserTasks.GetAll(u => u.AssignedUserId == userId);

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
