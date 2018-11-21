using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _497FinalProject.Models;

namespace _497FinalProject.Controllers
{
    public class ThreadController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Thread
        public ActionResult Index()
        {
            return View();
        }

        //// GET: Thread/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Thread/Create
        //[Authorize(Roles = "Professor")]
        public ActionResult CreateNewThread()
        {
            return View();
        }

        // POST: Thread/Create
        [HttpPost]
        //[Authorize(Roles = "Professor")]
        public ActionResult CreateNewThread(FormCollection collection, ThreadModel t )
        {
            if (ModelState.IsValid)
            {
                var thread = new ThreadModel {
                    ThreadName = t.ThreadName,
                    ThreadCategory = t.ThreadCategory,
                    ClassID = t.ClassID,
                    DateCreated = DateTime.Now,
                    DateOfLastPost = DateTime.Now,

                };
                db.Thread.Add(thread);
                db.SaveChanges();
            }
            return View(collection);
        }


        // GET: Thread/Delete/5
        //[Authorize(Roles = "Professor")]
        public ActionResult DeleteThread(int id)
        {
            return View();
        }

        // POST: Thread/Delete/5
        [HttpPost]
        //[Authorize(Roles = "Professor")]
        public ActionResult DeleteThread(FormCollection collection, ThreadModel t)
        {
            if (ModelState.IsValid)
            {
                var thread = new ThreadModel
                {
                    ThreadName = t.ThreadName,
                    ThreadCategory = t.ThreadCategory,
                    ClassID = t.ClassID,
                    DateCreated = DateTime.Now,
                };
                db.Thread.Remove(thread);
                db.SaveChanges();

            }
            return View(collection);
        }
    }
}
