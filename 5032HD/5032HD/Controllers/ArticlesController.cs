using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _5032HD.Models;
using Microsoft.AspNet.Identity;
using System.Collections;
using Microsoft.Ajax.Utilities;
using System.Web.Helpers;

namespace _5032HD.Controllers
{
    //[Authorize]
    public class ArticlesController : Controller
    {
        
        private Entities db = new Entities();

        public ActionResult Angular()
        {
            return View();
        }
        // GET: Articles
        public ActionResult Index()
        {
            var articles = db.Articles.Include(a => a.AspNetUser);
            return View(articles.ToList());
        }

        public ActionResult IndexJournalist()
        {
            string current = User.Identity.GetUserId();
            return View(db.Articles.Where(m => m.userID == current).ToList());
        }


        public ActionResult GeneratePdf()
        {
            return new Rotativa.MVC.ActionAsPdf("Index") {FileName ="Article List.pdf" };  
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email");
            Article article = new Article();
            string current = User.Identity.GetUserId();
            article.userID = current;
            return View(article);
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleID,title,datePub,description,comments,topic,userID")] Article article)
        {
            string current = User.Identity.GetUserId();
            article.userID = current;
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", article.userID);
            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", article.userID);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleID,title,datePub,description,comments,topic,userID")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", article.userID);
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
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

        public ActionResult Chart()
        {
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            var results = (from a in db.Articles select a);

            results.ToList().ForEach(rs => xValue.Add(rs.title));
            results.ToList().ForEach(rs => yValue.Add(rs.comments));
           
            new Chart(width: 600, height: 400, theme: ChartTheme.Green)

            .AddTitle("Popularity of articles")

            .AddSeries("Default", chartType: "column", xValue: xValue, yValues: yValue)

            .Write("bmp");
       
            //xValue.Add("Sports");
            //xValue.Add("Science");
            //xValue.Add("Business");
            //xValue.Add("Arts");

            //int count1 = db.Articles.Where(a => a.topic == "Sports").Count();
            //int count2 = db.Articles.Where(a => a.topic == "Science").Count();
            //int count3 = db.Articles.Where(a => a.topic == "Business").Count();
            //int count4 = db.Articles.Where(a => a.topic == "Arts").Count();

            //yValue.Add(count1);
            //yValue.Add(count2);
            //yValue.Add(count3);
            //yValue.Add(count4);

            //new Chart(width: 600, height: 400, theme: ChartTheme.Blue)
            //    .AddTitle("Number of articles in different topic")
            //    .AddSeries("Default", chartType:"Column",xValue: xValue, yValues: yValue)
            //    .Write("bmp");

            return null;
        }
    }
}
