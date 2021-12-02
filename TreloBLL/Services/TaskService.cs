using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Trelo1.Interfaces;
using TreloDAL.Models;
using AutoMapper;
using TreloBLL.DtoModel;
using TreloDAL.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using TreloBLL.Interfaces;
using Microsoft.Extensions.Logging;
using nClam;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Reflection;

namespace Trelo1.Services
{
    public class TaskService : ITaskService
    {
        private const int MaxFileCount = 5;
        private readonly TreloDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITaskFileService _taskFileService;
        private readonly IChangeTrackingService _changeTrackingService;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        

        public TaskService(TreloDbContext dbContext, 
            IMapper mapper, 
            ITaskFileService taskFileService, 
            IChangeTrackingService changeTrackingService,
            ILogger<TaskService> logger,
            IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _taskFileService = taskFileService;
            _changeTrackingService = changeTrackingService;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task AssignUserToTask(int taskId, int userId)
        {
            var task = await _dbContext.Tasks.Include(p => p.AssignedUser).FirstOrDefaultAsync(u => u.Id == taskId);
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            task.AssignedUser = user;
            _changeTrackingService.TrackChangeGeneric<UserTask, TaskChangesLog>(task, taskId);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Create(TaskDto userTaskDto, IList<IFormFile> formFiles, int? taskId)
        {
            if (taskId != null)
            {
                var task = await _dbContext.Tasks.Include(t => t.TaskFiles).AsNoTracking().FirstOrDefaultAsync(t => t.Id == taskId);

                if(task != null && formFiles != null)
                {
                    var files = _taskFileService.GenereteFilesForTask(formFiles);

                    if(task.TaskFiles.Count() + files.Count() > MaxFileCount)
                    {
                        return;
                    }

                    foreach (var file in files)
                    {
                        await CheckVirusesInFilesAsync(file);
                    }

                    userTaskDto.TaskFiles.AddRange(files);
                    task = _mapper.Map<UserTask>(userTaskDto);
                    task.Id = taskId.Value;
                    _changeTrackingService.TrackChangeGeneric<UserTask, TaskChangesLog>(task, task.Id);

                    _dbContext.Update(task);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else if (userTaskDto != null)
            {
                userTaskDto.TaskFiles = _taskFileService.GenereteFilesForTask(formFiles);
                foreach (var fileDto in userTaskDto.TaskFiles)
                {
                    //await CheckVirusesInFilesAsync(fileDto);
                }
                
                var userTask = _mapper.Map<UserTask>(userTaskDto);
                _dbContext.Tasks.Add(userTask);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> Delete(int id)
        {
            if (id != 0)
            {
                var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
                if (task != null)
                {
                    _dbContext.Tasks.Remove(task);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
            }

            return false;
        }

        public async Task<IEnumerable<TaskDto>> GetBoardTasks(int boardId)
        {
            if (boardId != 0)
            {
                var board = await _dbContext.Boards.Include(p => p.UserTasks).FirstOrDefaultAsync(b => b.Id == boardId);
                var taks = board.UserTasks;
                var boardTaskDto = _mapper.Map<List<TaskDto>>(taks);
                return boardTaskDto;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<TaskDto> GetOrganizationTasks(int organizationId)
        {
            if (organizationId != 0)
            {
                var boardInOrganization = _dbContext.Boards.Include(p => p.UserTasks).Where(o => o.OrganizationId == organizationId);

                List<UserTask> tasks = new List<UserTask>();
                foreach (var board in boardInOrganization)
                {
                    tasks.AddRange(board.UserTasks);
                }

                var taskDto = _mapper.Map<List<TaskDto>>(tasks);
                return taskDto;
            }
            else
            {
                return null;
            }
        }

        public async Task<TaskDto> GetTask(int taskId)
        {
            if (taskId != 0)
            {
                var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
                var taskDto = _mapper.Map<TaskDto>(task);
                return taskDto;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<TaskDto>> GetUserTasks(int userId)
        {
            if (userId != 0)
            {
                var user = await _dbContext.Users.Include(p => p.UserTasks).FirstOrDefaultAsync(t => t.Id == userId);
                var task = user.UserTasks;
                var tasksDto = _mapper.Map<List<TaskDto>>(task);
                return tasksDto;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> GetTaskChangeLogs(int taskId)
        {
            var taskLogs = await _dbContext.TaskChangesLogs.Where(p=>p.TaskId == taskId).ToArrayAsync();
            
            if (taskLogs != null)
            {
                string jsonTask = JsonConvert.SerializeObject(taskLogs, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return jsonTask;
            }
            return null;
        }

        private async Task CheckVirusesInFilesAsync(TaskFileDto taskFileDto)
        {
            try
            {
                _logger.LogInformation("ClamAV scan begin for file {0}", taskFileDto.FileName);
                var clam = new ClamClient(_configuration["ClamAVServer:URL"],
                                          Convert.ToInt32(this._configuration["ClamAVServer:Port"]));
                var scanResult = await clam.SendAndScanFileAsync(taskFileDto.DataFiles);
                switch (scanResult.Result)
                {
                    case ClamScanResults.Clean:
                        _logger.LogInformation("The file is clean! ScanResult:{1}", scanResult.RawResult);
                        break;
                    case ClamScanResults.VirusDetected:
                        _logger.LogError("Virus Found! Virus name: {1}", scanResult.InfectedFiles.FirstOrDefault().VirusName);
                        break;
                    case ClamScanResults.Error:
                        _logger.LogError("An error occured while scaning the file! ScanResult: {1}", scanResult.RawResult);
                        break;
                    case ClamScanResults.Unknown:
                        _logger.LogError("Unknown scan result while scaning the file! ScanResult: {0}", scanResult.RawResult);
                        break;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError("ClamAV Scan Exception: {0}", ex.ToString());
            }
            _logger.LogInformation("ClamAV scan completed for file {0}", taskFileDto.FileName);
        }
    }

    //block below its test block for comparing two object which isn't the same

    static class extentions
    {
        public static List<Variance> DetailedCompare<T>(this T val1, T val2)
        {
            List<Variance> variances = new List<Variance>();
            FieldInfo[] fi = val1.GetType().GetFields();
            foreach (FieldInfo f in fi)
            {
                Variance v = new Variance();
                v.Prop = f.Name;
                v.valA = f.GetValue(val1);
                v.valB = f.GetValue(val2);
                if (!Equals(v.valA, v.valB))
                    variances.Add(v);

            }
            return variances;
        }
    }
    class Variance
    {
        public string Prop { get; set; }
        public object valA { get; set; }
        public object valB { get; set; }
    }
}
