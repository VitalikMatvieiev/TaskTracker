using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TreloBLL.DtoModel;

namespace TreloBLL.Interfaces
{
    public interface ITaskFileService
    {
        Task ChangeFileName(int taskFileId, string newName);
        Task DeleteFileFromTask(int taskId, int taskFileId);
        List<TaskFileDto> GenereteFilesForTask(IList<IFormFile> formFiles);
        TaskFileDto GetFile(int fileId);

    }
}
