using Microsoft.EntityFrameworkCore;
using QuestionsOfRuneterra.Data;
using QuestionsOfRuneterra.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuestionsOfRuneterra.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ApplicationDbContext data;

        public ApplicationUserService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<string> GetProfileImagePaths()
        {
            return Directory
                .GetFiles(Directory.GetCurrentDirectory() + "\\wwwroot\\images\\icons", "*.*", SearchOption.AllDirectories)
                .Select(s => s.Replace(Directory.GetCurrentDirectory(), string.Empty))
                .Select(s => s.Replace("\\wwwroot", string.Empty))
                .Select(s => s.Replace("\\", "/"))
                .ToList();
        }

        public bool isUsernameUnique(string username)
        {
            return data.ApplicationUsers.All(au => au.UserName != username);
        }
    }
}
