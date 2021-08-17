using QuestionsOfRuneterra.Data;
using QuestionsOfRuneterra.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static QuestionsOfRuneterra.WebConstants;
using static QuestionsOfRuneterra.Areas.Admin.AdminConstants;
using Microsoft.AspNetCore.Identity;
using QuestionsOfRuneterra.Data.Models;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace QuestionsOfRuneterra.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ApplicationDbContext data;

        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationUserService(ApplicationDbContext data, UserManager<ApplicationUser> userManager)
        {
            this.data = data;
            this.userManager = userManager;
        }

        public string AdminId()
        {
            return data.ApplicationUsers.FirstOrDefaultAsync(au => bool.Parse(userManager.IsInRoleAsync(au, "Admin").ToString())).GetAwaiter().GetResult().Id;
        }

        public IEnumerable<string> GetProfileImagePaths()
        {
            return Directory
                .GetFiles(Directory.GetCurrentDirectory() + wwwrootPath + imagesPath + iconsPath, "*.*", SearchOption.AllDirectories)
                .Select(s => s.Replace(Directory.GetCurrentDirectory(), string.Empty))
                .Select(s => s.Replace(wwwrootPath, string.Empty))
                .Select(s => s.Replace("\\", "/"))
                .ToList();
        }

        public bool IsUsernameUnique(string username)
        {
            return data.ApplicationUsers.All(au => au.UserName != username);
        }

        public string UserName(string userId)
        {
            return data.ApplicationUsers.FirstOrDefault(au => au.Id == userId).UserName;
        }
    }
}
