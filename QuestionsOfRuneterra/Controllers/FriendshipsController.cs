using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestionsOfRuneterra.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace QuestionsOfRuneterra.Controllers
{
    [Authorize]
    public class FriendshipsController : Controller
    {
        public IActionResult All()
        {
            return View();
        }

        public IActionResult Suggestions()
        {
            return View();
        }

        public IActionResult MyFriends()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Remove(string id)
        {
            return View();
        }
    }
}
