using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TreloBLL.Interfaces
{
    public interface IFileService
    {
        string ConvertToByte64(IFormFile formFile);
    }
}
