using AutoMapper;
using Newtonsoft.Json;
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
        //цей метод тестовий, використовую для роботи дженеріковий
        public void TrackChange(UserTask updateEntity, int taskId)
        {
            var options = new JsonSerializerOptions()
            {
                IgnoreNullValues = true
            };
            string updateData = JsonConvert.SerializeObject(updateEntity, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

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
            string updateData = JsonConvert.SerializeObject(newEntity, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            var logDate = new LogGeneralData
            {
                ChangeTime = DateTime.Now,
                ChangeData = updateData,
                EntityId = entityId,
            };

            try
            {
                var logDateEntity = _mapper.Map<LogEntity>(logDate);

                _dbContext.Set<LogEntity>().Add(logDateEntity);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new InvalidCastException("Invalid entity for LogTracing");
            }
            
        }

    }
    public class LogGeneralData
    {
        public DateTime ChangeTime { get; set; }
        public string ChangeData { get; set; }
        public int EntityId { get; set; }
    }
}
