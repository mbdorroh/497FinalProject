using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using _497FinalProject.Models;

namespace _497FinalProject.Controllers
{
    public class ThreadController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public static List<ThreadModel> threadList = new List<ThreadModel> { };

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
        public ActionResult CreateNewThread(ThreadModel t, string id )
        {
            //check model is valid
            if (ModelState.IsValid)
            {
                //create thread model
                var thread = new ThreadModel {
                    ThreadName = t.ThreadName,
                    ThreadCategory = t.ThreadCategory,
                    ClassID = int.Parse(id),
                    DateCreated = DateTime.Now,
                    DateOfLastPost = DateTime.Now,

                };
                //validate class ID exists
                var dbToList = db.Class.ToList();
                while (!dbToList.Exists(x => x.ClassID == int.Parse(id)))
                {
                    ModelState.AddModelError("Validate", "Class ID does not exist.");
                    return View("CreateNewThread");
                }
                //increase no of threads for class ID
                foreach (var x in db.Class)
                {
                    if (x.ClassID== int.Parse(id))
                    {
                        x.NoOfThreads++;
                    }
                }
                //Add to database
                db.Thread.Add(thread);
                db.SaveChanges();
                return RedirectToAction("Index","Class");
            }
            return View(t);
        }


        // GET: Thread/Delete/5
        ////[Authorize(Roles = "Professor")]
        //public ActionResult DeleteThread()
        //{
        //    return View();
        //}

        // POST: Thread/Delete/5
        [HttpPost]
        //[Authorize(Roles = "Professor")]
        public ActionResult DeleteThread(  string id)
        {
            //check model is valid
            if (ModelState.IsValid)
            {
               
                //validate thread id exists
                var dbToList = db.Thread.ToList();
                while (!dbToList.Exists(x => x.ThreadID == int.Parse(id)))
                {
                    ModelState.AddModelError("Validate", "Thread ID does not exist.");
                    return View("DeleteThread");
                }
                //decrease no of threads per class
                var t = db.Thread.First(x => x.ThreadID == int.Parse(id));
                foreach (var x in db.Class)
                {
                    if (x.ClassID == int.Parse(id))
                    {
                        x.NoOfThreads--;
                    }
                }
                //remove from database
                db.Thread.Remove(t);
                db.SaveChanges();
                return RedirectToAction("Index", "Class");
            }
            return RedirectToAction("Index", "Class");
        }
        public ActionResult ViewThreadsByClass( string id) {
            //var _thread = new ThreadModel
            //{
            //    ThreadID = model.ThreadID,
            //    ThreadName = model.ThreadName,
            //    ClassID = model.ThreadID
            //};  //put threads in thread db table into a list for each class
            threadList.Clear();
            foreach (var x in db.Thread)
            {
                if (x.ClassID == int.Parse(id))
                {
                    threadList.Add(x);
                }
            }

//validate the class exists and redirect if not
            var dbToList = db.Thread.ToList();
            while (!dbToList.Exists(x => x.ClassID == int.Parse(id)))
            {

                return View("Index");
    }
            if (threadList == null)
            {
                return RedirectToAction("Index", "Class");
            }
            

            return View(threadList);
        }
    }
}
