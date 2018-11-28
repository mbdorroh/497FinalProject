namespace _497FinalProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<_497FinalProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(_497FinalProject.Models.ApplicationDbContext context)
        {
        //    context.Class.AddOrUpdate(x => x.ClassID,
        //new Models.ClassModel() { ClassID = 1, ClassName = "MIS 200" },
        //new Models.ClassModel() { ClassID = 2, ClassName = "MIS 220" },
        //new Models.ClassModel() { ClassID = 3, ClassName = "MIS 320" },
        //new Models.ClassModel() { ClassID = 4, ClassName = "MIS 330" },
        //new Models.ClassModel() { ClassID = 5, ClassName = "MIS 340" },
        //new Models.ClassModel() { ClassID = 6, ClassName = "MIS 430"  });

        //    context.Thread.AddOrUpdate(x => x.ThreadID,
        //new Models.ThreadModel(){ThreadID = 1, ThreadName = "Homework Help", ThreadCategory = "Homework", ClassID = 2, DateCreated= DateTime.Parse("11/26/2018") , DateOfLastPost = DateTime.Parse("11/27/2018"), NoOfPosts = 2},
        //new Models.ThreadModel() { ThreadID = 2, ThreadName = "Exam Help", ThreadCategory = "Exam", ClassID = 3, DateCreated = DateTime.Parse("11/26/2018"), DateOfLastPost = DateTime.Now, NoOfPosts = 1 },
        //new Models.ThreadModel() { ThreadID = 3, ThreadName = "Office Hours", ThreadCategory = "General Info", ClassID = 4, DateCreated = DateTime.Parse("11/26/2018"), DateOfLastPost = DateTime.Parse("11/28/2018"), NoOfPosts = 3 }
        //);

        //    context.Post.AddOrUpdate(x => x.PostID,
        //  new Models.PostModel() { PostID = 1, Subject = "Homework Help- Assignment 2", PostBody = "Please help me with question 3 on assingment 2 for MIS 200", ThreadID = 1 },
        //  new Models.PostModel() { PostID = 2, Subject = "Homework Help- Assignment 2", PostBody = "I also need help with question 3 on assingment 2 for MIS 200", ThreadID = 1 },
        //  new Models.PostModel() { PostID = 3, Subject = "Exam Help- Exam 1", PostBody = "What topics will be covered on MIS 320 Exam 1?", ThreadID = 2 },
        //  new Models.PostModel() { PostID = 4, Subject = "Office Hours", PostBody = "Are office hours for the TA cancelled today?", ThreadID = 3 },
        //  new Models.PostModel() { PostID = 5, Subject = "Office Hours", PostBody = "Will there be extra office hours before Exam 1?", ThreadID = 3 },
        //  new Models.PostModel() { PostID = 6, Subject = "Office Hours", PostBody = "What if I can't make it to office hours?", ThreadID = 3 });

        //    //add 2 teachers and 5 students
        //    context.Users.AddOrUpdate(x => x.Id,
        //    new Models.ApplicationUser() { },
        //    new Models.ApplicationUser() { },
        //    new Models.ApplicationUser() { },
        //    new Models.ApplicationUser() { },
        //    new Models.ApplicationUser() { },
        //    new Models.ApplicationUser() { },
        //    new Models.ApplicationUser() { }
        //    );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
