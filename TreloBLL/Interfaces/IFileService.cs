using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using TreloBLL.DtoModel;

namespace TreloBLL.Interfaces
{
    public interface IFileService
    {
        string ConvertToByte64(IFormFile formFile);
        void AddNewTypeFile(AllowedFileTypeDto allowedFileTypeDto);
        void ChangeTypeFile(int fileTypeId, AllowedFileTypeDto allowedFileTypeDto);
        List<FileGeneralDto> GenereteFileGeneral(IList<IFormFile> formFiles);
        bool HasAllowedDocument(string fileExtention, long fileSize);
    }
}
