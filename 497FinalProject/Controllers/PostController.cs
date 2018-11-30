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
    public class PostController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();
        public static List<PostModel> postList = new List<PostModel> { };
        

        //// GET: Post
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: Post/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Post/Create
        public ActionResult CreatePost()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        public ActionResult CreatePost(PostModel model, string id)
        {
            //check model is valid
            if (ModelState.IsValid)
            {

                //create post model
                var post = new PostModel
                {
                    PostUserName = model.PostUserName,
                    Subject = model.Subject,
                    PostBody = model.PostBody,
                    TimePost = DateTime.Now,
                    ThreadID = int.Parse(id)
                };
                //validate thread id
                var dbToList = db.Thread.ToList();
                while (!dbToList.Exists(x => x.ThreadID == post.ThreadID))
                {
                    ModelState.AddModelError("Validate", "Thread ID does not exist.");
                    return View("CreatePost");
                }
                //validate username
                var dbList = db.Users.ToList();
                while (!dbList.Exists(x => x.UserName == post.PostUserName))
                {
                    ModelState.AddModelError("Validate", "Username does not exist.");
                    return View("CreatePost");
                }
                //incremenet no of posts and update date
                foreach (var x in db.Thread)
                {
                    if (x.ThreadID == post.ThreadID)
                    {
                        x.NoOfPosts++;
                        x.DateOfLastPost = DateTime.Now;
                    }
                }
                //add to database
                db.Post.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index", "Class");
            }
            return View(model);
        }

        // GET: Post/Edit/5
        public ActionResult EditPost()
        {
            return View();
        }

        // POST: Post/Edit/5
        [HttpPost]
        public ActionResult EditPost(PostModel model, string id)
        {
            //check model is valid
            if (!ModelState.IsValid)
            {
                return View("EditPost", model);
            }
            //validate post id
            var dbToList = db.Post.ToList();
            while (!dbToList.Exists(x => x.PostID == int.Parse(id)))
            {
                ModelState.AddModelError("Validate", "Post ID does not exist.");
                return View("EditPost");
            }
            //update database with changes
            var p = db.Post.First(x => x.PostID == int.Parse(id));
            p.PostBody = model.PostBody;
            p.Subject = model.Subject;
            db.SaveChanges();
            return View();
        }

        // GET: All posts within one thread
        public ActionResult ViewPosts()
        {
            return View();
        }

        // POST: All posts within one thread
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ViewPosts(PostModel post)
        //{
        //    //add to list
        //    postList.Clear();
        //    foreach (var x in db.Post)
        //    {
        //        if (x.ThreadID == post.ThreadID)
        //        {
        //            postList.Add(x);
        //        }
        //    }
        //    //validate thread id
        //    var dbToList = db.Post.ToList();
        //    while (!dbToList.Exists(x => x.ThreadID == post.ThreadID))
        //    {
        //        return RedirectToAction("Index", "Class");
        //    }

        //    return RedirectToAction("ThreadPosts");
        //}



        //// GET: Tweets/AuthorTweets/
        //public ActionResult ThreadPosts()
        //{
        //    return View(postList);
        //}
        public ActionResult ViewPostsByThread(string id)
        {
            //var _thread = new ThreadModel
            //{
            //    ThreadID = model.ThreadID,
            //    ThreadName = model.ThreadName,
            //    ClassID = model.ThreadID
            //};  //put threads in thread db table into a list for each class
            postList.Clear();
            foreach (var x in db.Post)
            {
                if (x.ThreadID == int.Parse(id))
                {
                    postList.Add(x);
                }
            }

            //validate the class exists and redirect if not
            //var dbToList = db.Thread.ToList();
            //while (!dbToList.Exists(x => x.ClassID == int.Parse(id)))
            //{

            //    return View("Index");
            //}
            if (postList == null)
            {
                return RedirectToAction("Index", "Class");
            }
            

            return View(postList);
        }


        //// GET: Post/Delete/5
        //public ActionResult DeletePost()
        //{
        //    return View();
        //}

        // POST: Post/Delete/5
        [HttpPost]
        public ActionResult DeletePost( string id)
        {
            //check model is valid
            if (ModelState.IsValid)
            {
                
                //validate post id
                var list = db.Post.ToList();
                while (!list.Exists(x => x.PostID == int.Parse(id)))
                {
                    ModelState.AddModelError("Validate", "Post ID does not exist.");
                    return View("DeletePost");
                }
                ////validate thread id
                //var dbToList = db.Thread.ToList();
                //while (!dbToList.Exists(x => x.ThreadID == ))
                //{
                //    ModelState.AddModelError("Validate", "Thread ID does not exist.");
                //    return View("DeletePost");
                //}
                ////validate username
                //var dbList = db.User.ToList();
                //while (!dbList.Exists(x => x.UserName == post.PostUserName))
                //{
                //    ModelState.AddModelError("Validate", "Username does not exist.");
                //    return View("DeletePost");
                //}
                //decrease no of posts
                var p = db.Post.First(x => x.PostID == int.Parse(id));
                foreach (var x in db.Thread)
                {
                    if (x.ThreadID == p.ThreadID)
                    {
                        x.NoOfPosts--;
                    }
                }
                //remove from database
                db.Post.Remove(p);
                db.SaveChanges();
                return RedirectToAction("Index", "Class");
            }
            return RedirectToAction("Index", "Class");
        }
        public ActionResult AddApproval(string id)
        {
            var ID = int.Parse(id);
            var p = db.Post.First(x => x.PostID == ID);
            p.Approval++;
            db.SaveChanges();
            return RedirectToAction("ViewPostsByThread",  new { id = p.ThreadID });
        }
        public ActionResult AddDisapproval(string id)
        {
            var ID = int.Parse(id);
            var p = db.Post.First(x => x.PostID == ID);
            p.Disapproval++;
            db.SaveChanges();
            return RedirectToAction("ViewPostsByThread", new { id = p.ThreadID });
        }
        public ActionResult MakeSolution(string id)
        {
            var ID = int.Parse(id);
            var p = db.Post.First(x => x.PostID == ID);
            p.isSolution = true;
            db.SaveChanges();
            return RedirectToAction("ViewPostsByThread", new { id = p.ThreadID });
        }

    }
}
