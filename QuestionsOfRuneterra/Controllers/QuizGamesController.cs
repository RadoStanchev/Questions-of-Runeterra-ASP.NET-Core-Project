using Microsoft.AspNetCore.Mvc;
using QuestionsOfRuneterra.Services.Interfaces;
using QuestionsOfRuneterra.Models.QuizGames;
using QuestionsOfRuneterra.Infrastructure.Extensions;

namespace QuestionsOfRuneterra.Controllers
{
    public class QuizGamesController : Controller
    {
        private readonly IQuizGameService quizGameService;

        private readonly IQuizGameSessionService quizGameSessionService;

        public QuizGamesController(IQuizGameSessionService quizGameSessionService, IQuizGameService quizGameService)
        {
            this.quizGameSessionService = quizGameSessionService;
            this.quizGameService = quizGameService;
        }

        public IActionResult History()
        {
            return View();
        }

        public IActionResult Start()
        {
            return View(quizGameSessionService.Make(quizGameService.Start(User.Id())));
        }

        [HttpPost]
        public IActionResult Start([FromForm] quizGameSession)
        {

            return View();
        }

        public IActionResult Over(string id)
        {

            return View();
        }
    }
}
