using Microsoft.AspNetCore.Mvc;

namespace QuestionsOfRuneterra.Controllers
{
    public class QuizGamesController : Controller
    {
        public IActionResult History()
        {

            return View();
        }

        public IActionResult Start()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Start(string iztrigo)
        {

            return View();
        }

        public IActionResult Over(string id)
        {

            return View();
        }
    }
}
