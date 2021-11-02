using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreloBLL.DtoModel;
using TreloBLL.Interfaces;
using TreloDAL.Data;
using TreloDAL.Models;

namespace TreloBLL.Services
{
    public class TaskFileService : ITaskFileService
    {
        private readonly IMapper _mapper;
        private readonly TreloDbContext _dbContext;
        private readonly IFileService _fileService;
        public TaskFileService(IMapper mapper, TreloDbContext context, IFileService fileService)
        {
            _mapper = mapper;
            _dbContext = context;
            _fileService = fileService;
        }

        public async Task ChangeFileName(int taskFileId, string newName)
        {
            if (String.IsNullOrEmpty(newName))
            {
                return;
            }

            var taskFile = await _dbContext.TaskFiles.FirstOrDefaultAsync(f => f.DocumentId == taskFileId);
            if(taskFile != null)
            {
                taskFile.FileName = newName;
                _dbContext.Update(taskFile);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteFileFromTask(int taskId, int taskFileId)
        {
            var task = await _dbContext.Tasks.Include(t => t.TaskFiles).FirstOrDefaultAsync(t => t.Id == taskId);
            
            if(task != null)
            {
                var taskFile = task.TaskFiles.FirstOrDefault(f => f.DocumentId == taskFileId);
                if (taskFile != null)
                {
                    _dbContext.Remove(taskFile);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        public List<TaskFileDto> GenereteFilesForTask(IList<IFormFile> formFiles)
        {
            var files = _fileService.GenereteFileGeneral(formFiles);
            List<TaskFileDto> fileDtos = new List<TaskFileDto>();
            if (files != null)
            {
                fileDtos = _mapper.Map<List<TaskFileDto>>(files);
            }

            return fileDtos;
        }

        public TaskFileDto GetFile(int fileId)
        {
            var file = _dbContext.TaskFiles.FirstOrDefault(f => f.DocumentId == fileId);
            if (file != null)
            {
                var fileDto = _mapper.Map<TaskFileDto>(file);
                return fileDto;
            }

            return null;
        }
    }
}
