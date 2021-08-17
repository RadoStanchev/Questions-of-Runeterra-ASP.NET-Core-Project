using CarRentingSystem.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionsOfRuneterra.Infrastructure.Extensions;
using QuestionsOfRuneterra.Models.Answers;
using QuestionsOfRuneterra.Models.Questions;
using QuestionsOfRuneterra.Services.Interfaces;

namespace QuestionsOfRuneterra.Controllers
{
    [Authorize]
    public class QuestionsController : MyController
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

            SetOrderNumber();
            return RedirectToAction(nameof(AnswersController.Add), typeof(AnswersController).GetControllerName(),answerQuery);
        }


        public IActionResult Details([FromQuery] QuestionServiceModel question)
        {
            if (questionService.IsOwnedBy(question.Id, User.Id()) == false && User.IsAdmin() == false)
            {
                return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
            }

            return View(question);
        }

        public IActionResult Edit(string id)
        { 
            return View(questionService.Question(id));
        }

        public IActionResult Save([FromQuery] QuestionServiceModel question)
        {
            if(questionService.isOwnedBy(question.Id, User.Id()) == false && User.IsAdmin() == false)
            {
                return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
            }

            if (questionService.WrongAnswersCount(question.Id) < 3)
            {
                ModelState.AddModelError(nameof(question.Answers), "Wrong answers must be at least 3");
            }

            if (questionService.RightAnswersCount(question.Id) < 1)
            {
                ModelState.AddModelError(nameof(question.Answers), "Right answers must be at least 1");
            }

            questionService.Save(question.Id);

            return RedirectToAction(nameof(QuestionsController.Details));
        }

        [HttpPost]
        public IActionResult Edit([FromForm] QuestionServiceModel question)
        {
            if (questionService.isOwnedBy(question.Id, User.Id()) == false && User.IsAdmin() == false)
            {
                return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
            }

            questionService.Edit(question.Id, User.Id());
            var answerQuery = new AnswerQueryModel
            {
                QuestionId = question.Id
            };

            return RedirectToAction(nameof(AnswersController.Edit), typeof(AnswersController).GetControllerName(), answerQuery);
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
