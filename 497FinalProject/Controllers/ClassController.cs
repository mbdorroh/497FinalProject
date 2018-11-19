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
        // GET: Class
        public ActionResult Index()
        {
            return View(db.Classes.ToList());
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
        public ActionResult Create(FormCollection collection, ClassModel c)
        {
            if (ModelState.IsValid)
            {
                var thread = new ClassModel
                {
                    ClassName = c.ClassName,
                    ClassID = c.ClassID,
                    Professor = c.Professor
                    
                };
                db.SaveChanges();

            }
            return View(collection);
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
        public ActionResult DeleteClass(FormCollection collection, ClassModel c)
        {
            if (ModelState.IsValid)
            {
                var thread = new ClassModel
                {
                    ClassName = c.ClassName,
                    ClassID = c.ClassID,
                    Professor = c.Professor

                };
                db.Classes.Remove(c);
                db.SaveChanges();

            }
            return View(collection);
        }
    }
}
