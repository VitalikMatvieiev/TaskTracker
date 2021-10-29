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
        public TaskFileService(IMapper mapper, TreloDbContext context)
        {
            _mapper = mapper;
            _dbContext = context;
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
            List<TaskFileDto> fileDtos = new List<TaskFileDto>();

            foreach (var file in formFiles)
            {
                if (file?.Length > 0)
                {
                    var fileExtention = Path.GetExtension(file.FileName);
                    if (!HasAllowedDocument(fileExtention, file.Length))
                    {
                        continue;
                    }

                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()));
                    var contentType = file.ContentType;

                    var objFilesDto = new TaskFileDto()
                    {
                        FileName = newFileName,
                        FileType = fileExtention,
                        ContentType = contentType,
                    };

                    using (var target = new MemoryStream())
                    {
                        file.CopyTo(target);
                        objFilesDto.DataFiles = target.ToArray();
                        var objFile = _mapper.Map<TaskFile>(objFilesDto);
                        fileDtos.Add(objFilesDto);
                    }
                }
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

        private bool HasAllowedDocument(string fileExtention, long fileSize)
        {
            var allowedTypes = _dbContext.AllowedFileTypes.FirstOrDefault(f => f.FileType == fileExtention);
            if (allowedTypes != null)
            {
                return allowedTypes.AllowedSize >= fileSize / Math.Pow(10, 6) ? true : false;
            }

            return false;
        }
    }
}
