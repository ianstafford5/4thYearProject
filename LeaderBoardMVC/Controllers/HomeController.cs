using System;
using LeaderBoardMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Http.Results;

namespace LeaderBoardMVC.Controllers
{
    public class HomeController : Controller
    {
        private ScoreDB db = new ScoreDB();


       /* public void Register(RegisterViewModel model)
        {
            Debug.WriteLine("**************************Start Method****************************");
            Debug.WriteLine("EMAIL: " + model.Email + "PASSWORD: " + model.Password);
            if (ModelState.IsValid)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var newUser = new ApplicationUser() { Email = model.Email, UserName = model.Email };

                Debug.WriteLine("**************************Model Valid****************************");
                //Debug.WriteLine("Name: " + model.Email + "\nPassword: " + model.Password);
                var createUser = UserManager.Create(newUser, model.Password);

                if (createUser.Succeeded)
                {
                    Debug.WriteLine("**************************SUCCESS!!!****************************");
                }

            }
        }*/


        public ActionResult Index()
        {
            var model =
                from s in db.Scores
                orderby s.Score descending
                select s;

            return View(model);
        }

        
        public ActionResult About(FormModel form)
        {

            ScoreModel scores = new ScoreModel();
            scores.Name = form.Email;
            scores.Score = form.Score;
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