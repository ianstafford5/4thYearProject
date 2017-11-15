using LeaderBoardMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaderBoardMVC.Controllers
{
    public class HomeController : Controller
    {
        private ScoreDB db = new ScoreDB();

        public ActionResult Index()
        {
            var model =
                from s in db.Scores
                orderby s.Score descending
                select s;

            return View(model);
        }

        
        public ActionResult About(string name, int score)
        {
            ScoreModel scores = new ScoreModel();
            scores.Name = name;
            scores.Score = score;
            db.Scores.Add(scores);
            db.SaveChanges();
            ViewBag.Message = "Your application description page.";

            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}