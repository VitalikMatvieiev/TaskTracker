using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TreloBLL.Interfaces;

namespace TreloBLL.Services
{
    public class FileService : IFileService
    {
        public string ConvertToByte64(IFormFile formFile)
        {
            using (var ms = new MemoryStream())
            {
                formFile.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
                return s;
            }
        }
    }
}
