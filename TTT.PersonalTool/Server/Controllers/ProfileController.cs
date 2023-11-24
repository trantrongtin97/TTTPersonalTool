﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTT.PersonalTool.Contracts.IRepositories;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> logger;
        private readonly IUserRepository _userRepository;

        public ProfileController(ILogger<ProfileController> logger, IUserRepository userRepository)
        {
            this.logger = logger;
            this._userRepository = userRepository;
        }

        [HttpPut("updateprofile/{userId}")]
        public async Task<ActionResult<User>> UpdateProfile(int userId, [FromBody] User user)
        {
            User? userToUpdate = await _userRepository.GetByIdAsync(userId);
            if (userToUpdate != null)
            {
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                userToUpdate.Username = user.Username;
                userToUpdate.DateOfBirth = user.DateOfBirth;
                await _userRepository.GetContext().SaveChangesAsync();
            }

            return user;
        }

        [HttpGet("getprofile/{userId}")]
        public async Task<ActionResult<User>> GetProfile(int userId)
        {
            User? user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return new User();
            return ToCleanData(user);
        }

        [HttpGet("DownloadServerFile")]
        public async Task<ActionResult<string>> DownloadServerFile()
        {
            var filePath = @"C:\Data\";

            using (var fileInput = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                MemoryStream memoryStream = new MemoryStream();
                await fileInput.CopyToAsync(memoryStream);

                var buffer = memoryStream.ToArray();
                return Convert.ToBase64String(buffer);
            }
        }

        private static User ToCleanData(User user) =>
         new()
         {
             Id = user.Id,
             Username = user.Username,
             Password = "xxx",
             FirstName = user.FirstName,
             LastName = user.LastName,
             ProfilePictureUrl = user.ProfilePictureUrl,
             Role = user.Role,
             Theme = user.Theme,
             DateOfBirth = user.DateOfBirth,
             CreatedDate = user.CreatedDate
         };
    }
}
