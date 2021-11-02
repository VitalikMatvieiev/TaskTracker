using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TreloBLL.DtoModel;
using TreloBLL.Interfaces;
using TreloDAL.Data;
using TreloDAL.Models;

namespace TreloBLL.Services
{
    public class FileService : IFileService
    {
        private readonly TreloDbContext _dbContext;
        private readonly IMapper _mapper;
        public FileService(TreloDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void AddNewTypeFile(AllowedFileTypeDto allowedFileTypeDto)
        {
            if(allowedFileTypeDto != null)
            {
                var allowedFileType = _dbContext.AllowedFileTypes.FirstOrDefault(f => f.FileType == allowedFileTypeDto.FileType);

                if(allowedFileType == null)
                {
                    allowedFileType = _mapper.Map<AllowedFileTypes>(allowedFileTypeDto);
                    _dbContext.Add(allowedFileType);
                    _dbContext.SaveChanges();
                }
            }
        }

        public void ChangeTypeFile(int fileTypeId, AllowedFileTypeDto allowedFileTypeDto)
        {
            var fileType = _dbContext.AllowedFileTypes.AsNoTracking().FirstOrDefault(f => f.Id == fileTypeId);

            if (fileType != null)
            {
                fileType = _mapper.Map<AllowedFileTypes>(allowedFileTypeDto);
                fileType.Id = fileTypeId;
                _dbContext.Update(fileType);
                _dbContext.SaveChanges();
            }
        }

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

        public List<FileGeneralDto> GenereteFileGeneral(IList<IFormFile> formFiles)
        {
            List<FileGeneralDto> fileDtos = new List<FileGeneralDto>();

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

                    var objFilesDto = new FileGeneralDto()
                    {
                        FileName = newFileName,
                        FileType = fileExtention,
                        ContentType = contentType,
                    };

                    using (var target = new MemoryStream())
                    {
                        file.CopyTo(target);
                        objFilesDto.DataFile = target.ToArray();
                        fileDtos.Add(objFilesDto);
                    }
                }
            }
            return fileDtos;
        }
        public bool HasAllowedDocument(string fileExtention, long fileSize)
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
