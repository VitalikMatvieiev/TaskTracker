using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TreloDAL.Data;
using Trelo1.Interfaces;
using TreloDAL.Models;
using TreloBLL.DtoModel;
using AutoMapper;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Http;
using TreloBLL.Interfaces;

namespace Trelo1.Services
{
    public class UserService : IUserService
    {
        private readonly TreloDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;


        public UserService(TreloDbContext dbContext, IMapper mapper, IFileService fileService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task AddUserAvatar(int userId, IFormFile userAvatar)
        {
            var user = await _dbContext.Users.Include(u=>u.UserCredentialFiles).FirstOrDefaultAsync(u => u.Id == userId);

            if(userAvatar != null)
            {
                List<IFormFile> formFiles = new List<IFormFile>();
                formFiles.Add(userAvatar);

                var fileGeneral = _fileService.GenereteFileGeneral(formFiles).FirstOrDefault();
                if(fileGeneral == null)
                {
                    return;
                }

                var userCredentialFile = _mapper.Map<UserCredentialFile>(fileGeneral);
                userCredentialFile.AppFileType = CredetialFileType.Avatar;

                var curentAvatar = user.UserCredentialFiles.FirstOrDefault(f => f.AppFileType == CredetialFileType.Avatar);
                if (curentAvatar != null)
                {
                    user.UserCredentialFiles.Remove(curentAvatar);
                }

                user.UserCredentialFiles.Add(userCredentialFile);

                _dbContext.Update(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task Create(UserDto userDto)
        {
            if(userDto != null)
            {
                var user = _mapper.Map<User>(userDto);
                await _dbContext.Users.AddAsync(user);

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteUser(int userId)
        {
            if(userId != 0)
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null)
                {
                    _dbContext.Users.Remove(user);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
            }

            return false;
        }

        public IList<UserDto> GetAllUsers()
        {
            List<User> users = _dbContext.Users.ToList();
            List<UserDto> userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;
        }

        public async Task<User> GetUserData(string Email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == Email);
            return user;
        }

        public async Task<IList<UserDto>> GetUserInBoard(int boadrdId)
        {
            if(boadrdId != 0)
            {
                var board = await _dbContext.Boards.Include(p=>p.Users).FirstOrDefaultAsync(b=>b.Id == boadrdId);
                List<UserDto> userDtos = _mapper.Map<List<UserDto>>(board.Users);
                return userDtos;
            }
            else
            {
                return null;
            }
        }

        public async Task<IList<UserDto>> GetUserInOrganization(int organizationId)
        {
            if (organizationId != 0)
            {
                var organization = await _dbContext.Organizations.Include(p => p.Boards).FirstOrDefaultAsync(o => o.Id == organizationId);
                var boardInOrganization = organization.Boards.Where(u => u.Users != null);

                List <User> users = new List<User>();
                foreach (var board in boardInOrganization)
                {
                    users.AddRange(board.Users);
                }

                List<UserDto> userDtos = _mapper.Map<List<UserDto>>(users);
                return userDtos;
            }
            else
            {
                return null;
            }
        }

    }
}
