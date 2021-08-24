using CarRentingSystem.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using QuestionsOfRuneterra.Infrastructure.Extensions;
using QuestionsOfRuneterra.Models.QuizGames;
using QuestionsOfRuneterra.Services.Answers;
using QuestionsOfRuneterra.Services.QuizGame;
using QuestionsOfRuneterra.Services.QuizGameSessions;
using System.Linq;

namespace QuestionsOfRuneterra.Controllers
{
    public class QuizGamesController : Controller
    {
        private readonly IQuizGameService quizGameService;

        private readonly IQuizGameSessionService quizGameSessionService;

        private readonly IAnswerService answerService;

        public QuizGamesController(IQuizGameSessionService quizGameSessionService, IQuizGameService quizGameService, IAnswerService answerService)
        {
            this.quizGameSessionService = quizGameSessionService;
            this.quizGameService = quizGameService;
            this.answerService = answerService;
        }

        public IActionResult History([FromQuery] QuizGameQueryModel query)
        {
            var games = quizGameService.MyGames(
                User.Id(),
                query.Sorting,
                query.CurrentPage,
                QuizGameQueryModel.GamesPerPage);

            query.Games = games;
            query.TotalGames = games.Count();

            return View(query);

        }

        public IActionResult Details(string gameId)
        {
            return View(quizGameService.Details(gameId));
        }

        public IActionResult Start()
        {
            return View(quizGameSessionService.Make(quizGameService.Start(User.Id())));
        }

        [HttpPost]
        public IActionResult Start([FromForm] QuizGameSessionInputModel session)
        {
            if (quizGameService.IsPlayedBy(session.QuizGameId, User.Id()) == false && User.IsAdmin() == false)
            {
                return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
            }

            if (quizGameService.IsFinished(session.QuizGameId) == false)
            {
                return RedirectToAction(nameof(QuizGamesController.Details), session.QuizGameId);
            }

            if (answerService.Exists(session.AnswerId) == false)
            {
                ModelState.AddModelError("Answer", "There is no such a answer like this");
            }

            quizGameSessionService.AddAnswer(session.Id, session.AnswerId);

            if (quizGameSessionService.CanContinue(session.AnswerId, session.QuestionId) == false)
            {
                return this.RedirectToAction(nameof(QuizGamesController.Over), session.QuizGameId);
            }

            quizGameService.AddPoints(session.QuizGameId, session.OrdeNumber);

            if (quizGameService.IsThereMoreQuestions(session.QuizGameId) == false)
            {
                return RedirectToAction(nameof(QuizGamesController.Special), session.QuizGameId);
            }

            var next = quizGameSessionService.Make(session.QuizGameId);
            next.OrderNumber = ++session.OrdeNumber;

            return View(next);
        }

        public IActionResult Over(string gameId)
        {
            if (quizGameService.IsPlayedBy(gameId, User.Id()) == false && User.IsAdmin() == false)
            {
                return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
            }

            if (quizGameService.IsFinished(gameId) == false)
            {
                return RedirectToAction(nameof(QuizGamesController.Details), gameId);
            }

            quizGameService.Stop(gameId);

            return View(quizGameService.Details(gameId));
        }

        public IActionResult Special(string gameId)
        {
            if (quizGameService.IsThereMoreQuestions(gameId))
            {
                return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
            }

            return View(quizGameService.Details(gameId));
        }

    }
}
