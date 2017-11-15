using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeaderBoardMVC.Models;

namespace LeaderBoardMVC.Controllers
{
    public class ScoresController : Controller
    {
        private ScoreDB db = new ScoreDB();

        // GET: ScoreModels
        public ActionResult Index()
        {
            var model =
                from s in db.Scores
                orderby s.Score descending
                select s;

            return View(model);
        }

        // GET: ScoreModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScoreModel scoreModel = db.Scores.Find(id);
            if (scoreModel == null)
            {
                return HttpNotFound();
            }
            return View(scoreModel);
        }

        // GET: ScoreModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ScoreModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,Name,Score")] ScoreModel scoreModel)
        {
            if (ModelState.IsValid)
            {
                db.Scores.Add(scoreModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(scoreModel);
        }

        // GET: ScoreModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScoreModel scoreModel = db.Scores.Find(id);
            if (scoreModel == null)
            {
                return HttpNotFound();
            }
            return View(scoreModel);
        }

        // POST: ScoreModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Score")] ScoreModel scoreModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scoreModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(scoreModel);
        }

        // GET: ScoreModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScoreModel scoreModel = db.Scores.Find(id);
            if (scoreModel == null)
            {
                return HttpNotFound();
            }
            return View(scoreModel);
        }

        // POST: ScoreModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ScoreModel scoreModel = db.Scores.Find(id);
            db.Scores.Remove(scoreModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
