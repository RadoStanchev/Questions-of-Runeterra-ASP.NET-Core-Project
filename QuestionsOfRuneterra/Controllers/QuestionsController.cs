using CarRentingSystem.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using QuestionsOfRuneterra.Infrastructure.Extensions;
using QuestionsOfRuneterra.Models.Answers;
using QuestionsOfRuneterra.Models.Questions;
using QuestionsOfRuneterra.Services.Interfaces;

namespace QuestionsOfRuneterra.Controllers
{
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
        public IActionResult Add(QuestionFormModel question)
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


        public IActionResult Details([FromQuery] QuestionFormModel question)
        {
            if (questionService.isOwnedBy(question.Id, User.Id()) == false && User.IsAdmin() == false)
            {
                return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
            }

            return View();
        }

        public IActionResult Edit()
        {
            
            return View();
        }

        public IActionResult Save([FromQuery] QuestionFormModel question)
        {
            if(questionService.isOwnedBy(question.Id, User.Id()) == false)
            {
                return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
            }

            questionService.Save(question.Id);

            return RedirectToAction(nameof(QuestionsController.Details));
        }

        [HttpPost]
        public IActionResult Edit([FromQuery] QuestionFormModel question)
        {
            if (questionService.isOwnedBy(question.Id, User.Id()) == false)
            {
                return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
            }

            questionService.Edit(question.Id, User.Id());
            return RedirectToAction(nameof(AnswersController.Edit), typeof(AnswersController).GetControllerName());
        }

        public IActionResult Delete()
        {
            return View();
        }

        private void SetOrderNumber()
        {
            TempData["orderNumber"] = 0;
            TempData.Keep("orderNumber");
        }
    }
}
