using CarRentingSystem.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionsOfRuneterra.Infrastructure.Extensions;
using QuestionsOfRuneterra.Models.Answers;
using QuestionsOfRuneterra.Models.Questions;
using QuestionsOfRuneterra.Services.Questions;

namespace QuestionsOfRuneterra.Controllers
{
    [Authorize]
    public class QuestionsController : Controller
    {
        private readonly IQuestionService questionService;

        public QuestionsController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        public IActionResult All()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(QuestionServiceModel question)
        {
            if (!ModelState.IsValid)
            {
                return this.View(question);
            }

            var answerQuery = new AnswerQueryModel
            {
                QuestionId = questionService.Add(question.Content, User.Id())
            };

            return RedirectToAction(nameof(AnswersController.Add), typeof(AnswersController).GetControllerName(),answerQuery);
        }


        public IActionResult Details([FromQuery] QuestionServiceModel question)
        {
            if (questionService.IsOwnedBy(question.QuestionId, User.Id()) == false && User.IsAdmin() == false)
            {
                return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
            }

            return View(question);
        }

        public IActionResult Edit(string questionId, int currentPage)
        { 
            var question = questionService.Question(questionId);
            question.CurrentPage = currentPage;
            return View(question);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] QuestionServiceModel question)
        {
            if (questionService.IsOwnedBy(question.QuestionId, User.Id()) == false && User.IsAdmin() == false)
            {
                return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
            }

            questionService.Edit(question.QuestionId, question.Content);
            var answerQuery = new AnswerQueryModel
            {
                QuestionId = question.QuestionId
            };

            return RedirectToAction(nameof(AnswersController.Edit), typeof(AnswersController).GetControllerName(), answerQuery);
        }

        public IActionResult Save([FromQuery]QuestionServiceModel question)
        {
            if (questionService.IsOwnedBy(question.QuestionId, User.Id()) == false && User.IsAdmin() == false)
            {
                return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
            }

            if (questionService.WrongAnswersCount(question.QuestionId) < 3)
            {
                ModelState.AddModelError(nameof(question.Answers), "Wrong answers must be at least 3");
                return RedirectToAction(nameof(QuestionsController.Edit), question);
            }

            if (questionService.RightAnswersCount(question.QuestionId) < 1)
            {
                ModelState.AddModelError(nameof(question.Answers), "Right answers must be at least 1");
                return RedirectToAction(nameof(QuestionsController.Edit), question);
            }

            questionService.Save(question.QuestionId);

            return RedirectToAction(nameof(QuestionsController.Details));
        }

        public IActionResult Delete(string id)
        {
            if (questionService.IsOwnedBy(id, User.Id()) == false && User.IsAdmin() == false)
            {
                return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
            }

            questionService.Delete(id);
            return RedirectToAction(nameof(QuestionsController.All));
        }
    }
}
