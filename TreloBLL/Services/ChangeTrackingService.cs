using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TreloBLL.Interfaces;
using TreloDAL.Data;
using TreloDAL.Models;

namespace TreloBLL.Services
{
    public class ChangeTrackingService : IChangeTrackingService
    {
        private readonly TreloDbContext _dbContext;
        private readonly IMapper _mapper;
        public ChangeTrackingService(TreloDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void TrackChange(UserTask updateEntity, int taskId)
        {
            var options = new JsonSerializerOptions()
            {
                IgnoreNullValues = true
            };
            string updateData = JsonSerializer.Serialize(updateEntity, options);

            TaskChangesLog taskChangesLog = new TaskChangesLog()
            {
                ChangeTime = DateTime.Now,
                ChangeData = updateData,
                TaskId = taskId,

            };
            var trackModel = _dbContext.Add(taskChangesLog);
        }
        public void TrackChangeGeneric<TrackingEntity,LogEntity>(TrackingEntity newEntity, int entityId) where TrackingEntity : class where LogEntity : class
        {
            var options = new JsonSerializerOptions()
            {
                IgnoreNullValues = true
            };

            string updateData = JsonSerializer.Serialize(newEntity, options);

            var logDate = new
            {
                ChangeTime = DateTime.Now,
                ChangeData = updateData,
                EntityId = entityId,
            };

             var logDateEntity = _mapper.Map<LogEntity>(logDate);

            _dbContext.Set<LogEntity>().Add(logDateEntity);
            _dbContext.SaveChanges();
        }
    }
}
