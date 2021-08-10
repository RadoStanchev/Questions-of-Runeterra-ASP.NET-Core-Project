using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsOfRuneterra.Controllers
{
    public class FriendshipsController : Controller
    {
        public IActionResult All()
        {
            return View();
        }

        public IActionResult Profile()
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
