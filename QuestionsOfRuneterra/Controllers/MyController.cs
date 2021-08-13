using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsOfRuneterra.Controllers
{
    public abstract class MyController : Controller
    {
        protected void IncreaseOrderNumber()
        {
            TempData["orderNumber"] = int.Parse(TempData["orderNumber"].ToString()) + 1;
            TempData.Keep("orderNumber");
        }

        protected void DecreaseOrderNumber()
        {
            TempData["orderNumber"] = int.Parse(TempData["orderNumber"].ToString()) - 1;
            TempData.Keep("orderNumber");
        }
    }
}
