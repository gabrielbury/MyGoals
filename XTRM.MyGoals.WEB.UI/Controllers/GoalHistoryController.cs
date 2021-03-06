﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XTRM.MyGoals.WEB.UI.Controllers
{
    public class GoalHistoryController : Controller
    {
        /// <summary>
        /// List the history of specific Goal
        /// </summary>
        /// <param name="goalId"></param>
        /// <returns></returns>
        public ActionResult GoalHistoryList(Guid goalId)
        {
            var model = new List<Models.GoalHistory>();
            using (var contexto = new Models.MyGoalsEntities())
            {
                model = contexto.GoalHistory
                    .Include("Goal")
                    .Where(gh => gh.idGoal == goalId)
                    .ToList();
            }
            return View(model);
        }

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
            return RedirectToAction("GoalHistoryList", new { @goalId = _goal.Goal.id });
        }

        public ActionResult EditHistory(Guid goalHistoryId)
        {
            var model = new Models.GoalHistory();
            using (var contexto = new Models.MyGoalsEntities())
            {
                model = contexto.GoalHistory.Include("Goal").FirstOrDefault(gh => gh.id == goalHistoryId);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditHistory(Models.GoalHistory _goalH)
        {
            var model = new Models.GoalHistory();
            try
            {
                using (var contexto = new Models.MyGoalsEntities())
                {
                    model = contexto.GoalHistory.Include("Goal").FirstOrDefault(gh => gh.id == _goalH.id);
                    model.description = _goalH.description;
                    contexto.SaveChanges();
                }
            }catch(Exception erro)
            {
                ViewBag.Erro = erro.Message;
            }
            return View(model);
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