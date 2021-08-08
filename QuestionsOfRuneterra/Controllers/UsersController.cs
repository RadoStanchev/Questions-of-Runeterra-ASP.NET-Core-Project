using CarRentingSystem.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuestionsOfRuneterra.Data.Models;
using System.Threading.Tasks;

namespace QuestionsOfRuneterra.Controllers
{
    public class UsersController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        public UsersController(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public IActionResult Logout()
        {
            Task.Run(async () =>
            {
                await signInManager.SignOutAsync();
            })
            .GetAwaiter()
            .GetResult();

            return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
        }
    }
}
