using System;
using System.Linq;
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
        public ActionResult CreateNewThread(ThreadModel t )
        {
            //check model is valid
            if (ModelState.IsValid)
            {
                //create thread model
                var thread = new ThreadModel {
                    ThreadName = t.ThreadName,
                    ThreadCategory = t.ThreadCategory,
                    ClassID = t.ClassID,
                    DateCreated = DateTime.Now,
                    DateOfLastPost = DateTime.Now,

                };
                //validate class ID exists
                var dbToList = db.Class.ToList();
                while (!dbToList.Exists(x => x.ClassID == t.ClassID))
                {
                    ModelState.AddModelError("Validate", "Class ID does not exist.");
                    return View("CreateNewThread");
                }
                //increase no of threads for class ID
                foreach (var x in db.Class)
                {
                    if (x.ClassID== t.ClassID)
                    {
                        x.NoOfThreads++;
                    }
                }
                //Add to database
                db.Thread.Add(thread);
                db.SaveChanges();
            }
            return View(t);
        }


        // GET: Thread/Delete/5
        //[Authorize(Roles = "Professor")]
        public ActionResult DeleteThread()
        {
            return View();
        }

        // POST: Thread/Delete/5
        [HttpPost]
        //[Authorize(Roles = "Professor")]
        public ActionResult DeleteThread( ThreadModel model)
        {
            //check model is valid
            if (ModelState.IsValid)
            {
                //create thread model
                var thread = new ThreadModel
                {
                    ThreadID = model.ThreadID,
                    ThreadName = model.ThreadName,
                };
                //validate thread id exists
                var dbToList = db.Thread.ToList();
                while (!dbToList.Exists(x => x.ThreadID == model.ThreadID))
                {
                    ModelState.AddModelError("Validate", "Thread ID does not exist.");
                    return View("DeleteThread");
                }
                //decrease no of threads per class
                var t = db.Thread.First(x => x.ThreadID == thread.ThreadID);
                foreach (var x in db.Class)
                {
                    if (x.ClassID == model.ClassID)
                    {
                        x.NoOfThreads--;
                    }
                }
                //remove from database
                db.Thread.Remove(t);
                db.SaveChanges();

            }
            return View(model);
        }
    }
}
