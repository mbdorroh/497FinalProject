using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _497FinalProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace _497FinalProject.Controllers
{
    public class ClassController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public static List<ThreadModel> threadList  = new List<ThreadModel> { };

        // GET: Class
        public ActionResult Index()
        {
            //get list of classes
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
           
            //check model is valid
            if (ModelState.IsValid)
            {
                //create class model
                var c = new ClassModel
                {
                    ClassName = model.ClassName,
                    
                };
          
                //add to database
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
            //put threads in thread db table into a list for each class
            threadList.Clear();
            foreach (var x in db.Thread)
            {
                if (x.ClassID == thread.ClassID)
                {
                    threadList.Add(x);
                }
            }

            //validate the class exists and redirect if not
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
            //check model is valid
            if (ModelState.IsValid)
            {
                //create class model
                var c = new ClassModel
                {
                    ClassName = model.ClassName,
                    ClassID = model.ClassID,
                    ProfessorID = model.ProfessorID

                };
                //validate class ID exists
                var dbToList = db.Class.ToList();
                while (!dbToList.Exists(x => x.ClassID == c.ClassID))
                {
                    ModelState.AddModelError("Validate", "Class ID does not exist.");
                    return View("DeleteClass");
                }
                var index = db.Class.First(x => x.ClassID == c.ClassID);
                //remove from database
                db.Class.Remove(index);
                db.SaveChanges();
                return RedirectToAction("Index", "Class");
            }
            return View(model);
        }

        //Allows user to apply for approval to join a class
        public ActionResult JoinClass(ClassModel c)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //Get the user
            var user = UserManager.FindByName(User.Identity.Name);
            var u = new UserModel()
            {
                Name = user.FirstName + user.LastName,
                UserID = Int32.Parse(user.Id),
            };
            c.Users.Add(u);

            return View();
            
        }


        public ActionResult ApproveClass(ClassModel c)
        {
            return View(c.Users);
        }

    }
}
