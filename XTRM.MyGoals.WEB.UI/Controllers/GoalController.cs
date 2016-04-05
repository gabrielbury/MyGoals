using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XTRM.MyGoals.WEB.UI.Controllers
{
    public class GoalController : Controller
    {
        // GET: Goal
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Include()
        {
            return View();
        }

        /// <summary>
        /// Includes a new Goal
        /// </summary>
        /// <param name="goal"> the new goal to register</param>
        /// <returns> </returns>
        [HttpPost]
        public JsonResult Include(Models.Goal goal)
        {
            using (var context = new Models.MyGoalsEntities())
            {
                Guid id = goal.id = Guid.NewGuid();
                goal.includeDate = DateTime.Now;
                context.Goal.Add(goal);

                try
                {
                    context.SaveChanges();
                    return Json(new Models.Return()
                    {
                        ID = goal.id,
                        Code = (int)Models.Enums.ReturnCode.SUCESSO,
                        Date = DateTime.Now.ToLongDateString(),
                        Message = "Success",
                        Path = ""
                    });
                }
                catch (Exception e)
                {
                    return Json(new Models.Return()
                    {
                        ID = Guid.Empty,
                        Code = (int)Models.Enums.ReturnCode.ERRO,
                        Date = DateTime.Now.ToLongDateString(),
                        Message = e.Message,
                        Path = ""
                    });
                }
            }


        }

        /// <summary>
        /// Gets the registered goals
        /// </summary>
        [HttpGet]
        public ActionResult GoalsList()
        {
            using (var context = new Models.MyGoalsEntities())
            {
                var goalsList = context.Goal.OrderBy(g => g.includeDate).ToList();
                return View(goalsList);
            }

        }
    }
}