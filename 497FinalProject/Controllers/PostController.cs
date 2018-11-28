using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _497FinalProject.Models;


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
        public ActionResult CreatePost(PostModel model)
        {
            if (ModelState.IsValid)
            {
                var post = new PostModel
                {
                   PostUserName = model.PostUserName,
                   Subject = model.Subject,
                   PostBody = model.PostBody,
                   TimePost = DateTime.Now,
                   ThreadID = model.ThreadID
                };

                var dbToList = db.Thread.ToList();
                while (!dbToList.Exists(x => x.ThreadID == post.ThreadID))
                {
                    ModelState.AddModelError("Validate", "Thread ID does not exist.");
                    return View("CreatePost");
                }
                var dbList = db.User.ToList();
                while (!dbList.Exists(x => x.UserName == post.PostUserName))
                {
                    ModelState.AddModelError("Validate", "Username does not exist.");
                    return View("CreatePost");
                }
                foreach (var x in db.Thread)
                {
                    if (x.ThreadID == post.ThreadID)
                    {
                        x.NoOfPosts++;
                        x.DateOfLastPost = DateTime.Now;
                    }
                }
                db.Post.Add(post);
                db.SaveChanges();
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
        public ActionResult EditPost(PostModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditPost", model);
            }
            var dbToList = db.Post.ToList();
            while (!dbToList.Exists(x => x.PostID == model.PostID))
            {
                ModelState.AddModelError("Validate", "Post ID does not exist.");
                return View("EditPost");
            }
            var p = db.Post.First(x => x.PostID == model.PostID);
            p.PostBody = model.PostBody;
            p.Subject = model.Subject;
            db.SaveChanges();
            return View(model);
        }

        // GET: All posts within one thread
        public ActionResult ViewPosts()
        {
            return View();
        }

        // POST: All posts within one thread
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ViewPosts(PostModel post)
        {
            postList.Clear();
            foreach (var x in db.Post)
            {
                if (x.ThreadID == post.ThreadID)
                {
                    postList.Add(x);
                }
            }

            var dbToList = db.Post.ToList();
            while (!dbToList.Exists(x => x.ThreadID == post.ThreadID))
            {
                return RedirectToAction("Index", "Class");
            }

            return RedirectToAction("ThreadPosts");
        }

        // GET: Tweets/AuthorTweets/
        public ActionResult ThreadPosts()
        {
            return View(postList);
        }

        // GET: Post/Delete/5
        public ActionResult DeletePost()
        {
            return View();
        }

        // POST: Post/Delete/5
        [HttpPost]
        public ActionResult DeletePost(PostModel model)
        {
            if (ModelState.IsValid)
            {
                var post = new PostModel
                {
                  PostID = model.PostID,
                  ThreadID = model.ThreadID,
                  PostUserName = model.PostUserName
                };
                var list = db.Post.ToList();
                while (!list.Exists(x => x.PostID == model.PostID))
                {
                    ModelState.AddModelError("Validate", "Post ID does not exist.");
                    return View("DeletePost");
                }
                var dbToList = db.Thread.ToList();
                while (!dbToList.Exists(x => x.ThreadID == post.ThreadID))
                {
                    ModelState.AddModelError("Validate", "Thread ID does not exist.");
                    return View("DeletePost");
                }
                var dbList = db.User.ToList();
                while (!dbList.Exists(x => x.UserName == post.PostUserName))
                {
                    ModelState.AddModelError("Validate", "Username does not exist.");
                    return View("DeletePost");
                }
                var p = db.Post.First(x => x.PostID == post.PostID);
                foreach (var x in db.Thread)
                {
                    if (x.ThreadID == post.ThreadID)
                    {
                        x.NoOfPosts--;
                    }
                }
                db.Post.Remove(p);
                db.SaveChanges();

            }
            return View(model);
        }
    }
}
