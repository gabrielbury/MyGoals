using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XTRM.MyGoals.WEB.UI.Controllers
{
    public class GoalHistoryController : Controller
    {
        // GET: GoalHistory
        public ActionResult AddHistory(Guid goalId)
        {
            Models.GoalHistory goalHistory = new Models.GoalHistory();
            using (var contexto = new Models.MyGoalsEntities())
            {
                goalHistory.Goal = contexto.Goal.FirstOrDefault(g => g.id == goalId);
            }
            return View(goalHistory);
        }

        [HttpPost]
        public ActionResult AddHistory(Models.GoalHistory _goal)
        {
            using (var contexto = new Models.MyGoalsEntities())
            {
                contexto.GoalHistory.Add(
                    new Models.GoalHistory()
                    {
                        idGoal = _goal.Goal.id,
                        description = _goal.description,
                        id = Guid.NewGuid(),
                        includeDate = _goal.includeDate
                    }
                );
                contexto.SaveChanges();
            }
            return View();
        }

        [HttpDelete]
        public ActionResult DeleteHistoryItem(Guid goalId)
        {
            using (var contexto = new Models.MyGoalsEntities())
            {
                contexto.GoalHistory
                    .Remove(contexto.GoalHistory.FirstOrDefault(gh => gh.id == goalId));
                contexto.SaveChanges();
            }
            return View();
        }
    }
}