using Microsoft.AspNetCore.Mvc;
using QuestionsOfRuneterra.Infrastructure.Extensions;
using QuestionsOfRuneterra.Models.Answers;
using QuestionsOfRuneterra.Services.Interfaces;
using static QuestionsOfRuneterra.WebConstants;

namespace QuestionsOfRuneterra.Controllers
{
    public class AnswersController : MyController
    {
        private IAnswerService answerService;

        public AnswersController(IAnswerService answerService)
        {
            this.answerService = answerService;
        }

        public IActionResult Add([FromQuery] AnswerQueryModel query)
        {
            query.TotalAnswers = answerService.TotalAnswersToQuestion(query.QuestionId);
            return View(query);
        }

        [HttpPost]
        public IActionResult Add([FromForm] AnswerServiceModel answer, [FromQuery] bool increaseOrderNumber)
        {
            return this.View(Redaction(answer, RedactionType.Add, increaseOrderNumber));
        }

        public IActionResult Edit([FromQuery] AnswerQueryModel query)
        {
            DecreaseOrderNumber();
            query.CurrentAnswer = answerService.PreviousAnswer(query.QuestionId, OrderNumber());
            query.PreviousAnswer = OrderNumber()-1 < 0 ? null : answerService.PreviousAnswer(query.QuestionId, OrderNumber()-1);
            query.TotalAnswers = answerService.TotalAnswersToQuestion(query.QuestionId);
            return View(query);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] AnswerServiceModel answer, [FromQuery] bool increaseOrderNumber)
        {
            var query = Redaction(answer, RedactionType.Edit, increaseOrderNumber);
            if (OrderNumber() == answerService.TotalAnswersToQuestion(answer.QuestionId))
            {
                this.RedirectToAction(nameof(AnswersController.Add), query);
            }

            return this.View(query);
        }

        private AnswerQueryModel Redaction(AnswerServiceModel answer, RedactionType type, bool increaseOrderNumber)
        {
            AnswerQueryModel query;
            if (!ModelState.IsValid)
            {
                query = new AnswerQueryModel
                {
                    PreviousAnswer = OrderNumber() == 0 ? null : answerService.PreviousAnswer(answer.QuestionId, OrderNumber()),
                    CurrentAnswer = answer,
                    QuestionId = answer.QuestionId,
                    TotalAnswers = answerService.TotalAnswersToQuestion(answer.QuestionId)
                };
            }
            else
            {
                if (type == RedactionType.Add)
                {
                    answerService.Add(answer.Content, answer.IsRight, answer.QuestionId, User.Id());
                }
                else if (type == RedactionType.Edit)
                {
                    answerService.Edit(answer.Id, answer.Content, answer.IsRight);
                }

                query = new AnswerQueryModel
                {
                    PreviousAnswer = answer,
                    QuestionId = answer.QuestionId,
                    TotalAnswers = answerService.TotalAnswersToQuestion(answer.QuestionId)
                };

                if (increaseOrderNumber == true)
                {
                    IncreaseOrderNumber();
                    query.CurrentAnswer = OrderNumber() == answerService.TotalAnswersToQuestion(answer.QuestionId) ? null : answerService.PreviousAnswer(answer.QuestionId, OrderNumber());
                }
                else
                {
                    DecreaseOrderNumber();
                    query.CurrentAnswer = OrderNumber() == answerService.TotalAnswersToQuestion(answer.QuestionId) ? null : answerService.NextAnswer(answer.QuestionId, OrderNumber());
                }
               
            }

            return query;
        }

        private int OrderNumber()
        {
            var orderNumber = int.Parse(TempData["orderNumber"].ToString());
            TempData.Keep("orderNumber");
            return orderNumber;
        }

    }
}
