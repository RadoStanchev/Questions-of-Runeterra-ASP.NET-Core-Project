using CarRentingSystem.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using QuestionsOfRuneterra.Infrastructure.Extensions;
using QuestionsOfRuneterra.Models.Answers;
using QuestionsOfRuneterra.Models.Questions;
using QuestionsOfRuneterra.Services.Answers;
using static QuestionsOfRuneterra.WebConstants;

namespace QuestionsOfRuneterra.Controllers
{
    public class AnswersController : Controller
    {
        private IAnswerService answerService;

        public AnswersController(IAnswerService answerService)
        {
            this.answerService = answerService;
        }

        public IActionResult Add([FromQuery] AnswerQueryModel query)
        {
            return View(query);
        }

        [HttpPost]
        public IActionResult Add([FromQuery] AnswerQueryModel query, [FromForm] AnswerServiceModel answer, bool increaseCurrentPage)
        {
            if (query.CurrentPage - 1 < query.TotalAnswers)
            {
                return Edit(query, answer, increaseCurrentPage);
            }

            if (!TryValidateModel(answer))
            {
                if(increaseCurrentPage == false)
                {
                    query.CurrentPage -= 1;

                    return RedirectToAction(nameof(AnswersController.Edit), query);
                }

                return View(query);
            }

            answerService.Add(answer.Content, answer.IsRight, answer.QuestionId, User.Id());

            query.CurrentPage = increaseCurrentPage == true ?
                query.CurrentPage + 1 : query.CurrentPage - 1;
            query.TotalAnswers = answerService.TotalAnswersToQuestion(query.QuestionId);

            if (query.CurrentPage - 1 < query.TotalAnswers)
            {
                query.CurrentAnswer = answerService.Answer(query.QuestionId, query.CurrentPage);
                return RedirectToAction(nameof(AnswersController.Edit), query);
            }

            return View(query);
        }

        public IActionResult Edit(AnswerQueryModel query)
        {
            query.CurrentAnswer = answerService.Answer(query.QuestionId, query.CurrentPage);
            return View(query);
        }

        [HttpPost]
        public IActionResult Edit([FromQuery] AnswerQueryModel query, [FromForm] AnswerServiceModel answer, bool increaseCurrentPage)
        {
            if (query.CurrentPage - 1 >= query.TotalAnswers)
            {
                return Add(query, answer, increaseCurrentPage);
            }

            if (!TryValidateModel(answer))
            {
                return View(query);
            }

            answerService.Edit(answer.Id,answer.Content, answer.IsRight);

            query.CurrentPage = increaseCurrentPage == true ?
                query.CurrentPage + 1 : query.CurrentPage - 1;
            query.TotalAnswers = answerService.TotalAnswersToQuestion(query.QuestionId);

            if (query.CurrentPage - 1 >= query.TotalAnswers)
            {
                return RedirectToAction(nameof(AnswersController.Add), query);
            }

            query.CurrentAnswer = answerService.Answer(query.QuestionId, query.CurrentPage);

            return View(query);
        }

        public IActionResult Delete([FromQuery] AnswerServiceModel answer)
        {
            if (answerService.IsOwnedBy(answer.Id, User.Id()) == false && User.IsAdmin() == false)
            {
                return RedirectToAction(nameof(HomeController.Index), typeof(HomeController).GetControllerName());
            }
            answerService.Delete(answer.Id);

            var query = new QuestionServiceModel
            {
                QuestionId = answer.QuestionId
            };

            return this.RedirectToAction(nameof(QuestionsController.Details), typeof(QuestionsController).GetControllerName(), query);
        }
    }
}
