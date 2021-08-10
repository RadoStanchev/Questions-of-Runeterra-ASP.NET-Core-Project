using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsOfRuneterra.Controllers
{
    public class LeaderBoardApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
