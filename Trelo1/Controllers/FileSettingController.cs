using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloBLL.DtoModel;
using TreloBLL.Interfaces;

namespace Trelo1.Controllers
{
    [ApiController]
    [Authorize]
    public class FileSettingController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileSettingController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        [Route("api/file-setting/add-new")]
        public IActionResult AddNewFileTypes(AllowedFileTypeDto allowedFileTypeDto)
        {
            if(allowedFileTypeDto != null)
            {
                _fileService.AddNewTypeFile(allowedFileTypeDto);
                return Ok();
            }

            return NoContent();
        }

        [HttpPost]
        [Route("api/file-setting/change/{fileTypeId}")]
        public IActionResult ChangeFileTypes(int fileTypeId, AllowedFileTypeDto allowedFileTypeDto)
        {
            if (allowedFileTypeDto != null)
            {
                _fileService.ChangeTypeFile(fileTypeId, allowedFileTypeDto);
                return Ok();
            }

            return NoContent();
        }
    }
}
