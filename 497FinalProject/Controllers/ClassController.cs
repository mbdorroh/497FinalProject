using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _497FinalProject.Models;

namespace _497FinalProject.Controllers
{
    public class ClassController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public static List<ThreadModel> threadList  = new List<ThreadModel> { };

        // GET: Class
        public ActionResult Index()
        {
            var classes = db.Class.ToList();
            return View(classes);
        }

        //// GET: Class/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Class/Create
        //[Authorize(Roles = "Professor")]
        public ActionResult CreateNewClass()
        {
            return View();
        }

        // POST: Class/Create
        [HttpPost]
        //[Authorize(Roles = "Professor")]
        public ActionResult CreateNewClass(ClassModel model)
        {
            if (ModelState.IsValid)
            {
                var c = new ClassModel
                {
                    ClassName = model.ClassName,
                    ProfessorID =model.ProfessorID
                    
                };
                //var dbToList = db.Class.ToList();
                //while (!dbToList.Exists(x => x.ProfessorID == c.ProfessorID))
                //{
                //    ModelState.AddModelError("Validate", "Professor ID does not exist.");
                //    return View("CreateNewClass");
                //}
                db.Class.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index", "Class");
            }
            return View(model);
        }

        // GET: All threads within one class
        public ActionResult ViewAllThreads()
        {
            return View();
        }

        // POST: All threads within one class
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ViewAllThreads(ThreadModel thread)
        {
            threadList.Clear();
            foreach (var x in db.Thread)
            {
                if (x.ClassID == thread.ClassID)
                {
                    threadList.Add(x);
                }
            }

            var dbToList = db.Thread.ToList();
            while (!dbToList.Exists(x => x.ClassID == thread.ClassID))
            {
                
                return View("Index");
            }
            if (threadList == null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("ClassThreads");
        }

        // GET: Tweets/AuthorTweets/
        public ActionResult ClassThreads()
        {
            return View(threadList);
        }


        // GET: Class/Delete/5
        //[Authorize(Roles = "Professor")]
        public ActionResult DeleteClass()
        {
            return View();
        }

        // POST: Class/Delete/5
        [HttpPost]
        //[Authorize(Roles = "Professor")]
        public ActionResult DeleteClass( ClassModel model)
        {
            if (ModelState.IsValid)
            {
                var c = new ClassModel
                {
                    ClassName = model.ClassName,
                    ClassID = model.ClassID,
                    ProfessorID = model.ProfessorID

                };
                var dbToList = db.Class.ToList();
                while (!dbToList.Exists(x => x.ClassID == c.ClassID))
                {
                    ModelState.AddModelError("Validate", "Class ID does not exist.");
                    return View("DeleteClass");
                }
                var index = db.Class.First(x => x.ClassID == c.ClassID);
                db.Class.Remove(index);
                db.SaveChanges();

            }
            return View(model);
        }
    }
}
