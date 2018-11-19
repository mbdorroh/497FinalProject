namespace _497FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
               "dbo.Class",
               c => new
               {
                   ClassID = c.Int(nullable: false),
                   ClassName = c.String(nullable: false, maxLength: 256),
                   Professor = c.Int(nullable: false),
                   ThreadID = c.Int(nullable: false)
               })
               .PrimaryKey(t => t.ClassID);

            CreateTable(
                "dbo.Thread",
                c => new
                {
                    ThreadID = c.Int(nullable: false),
                    ThreadName = c.String(nullable: false, maxLength: 256),
                    ThreadCategory = c.String(nullable: false, maxLength: 256),
                    NoOfPosts = c.Int(),
                    DateCreated = c.DateTime(),
                    DateOfLastPost = c.DateTime(),
                    ClassID = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.ThreadID })
                .ForeignKey("dbo.Class", t => t.ClassID, cascadeDelete: true);


            CreateTable(
                "dbo.Post",
                c => new
                {
                    PostID = c.Int(nullable: false),
                    Subject = c.String(nullable: false, maxLength: 256),
                    PostBody = c.String(),
                    isSolution = c.Boolean(),
                    PostUserName = c.String(),
                    PostUserType = c.String(),
                    PhoneNumber = c.String(),
                    Comment = c.String(),
                    TimePosted = c.DateTime(),
                    Approval = c.Int(),
                    Disapproval = c.Int(),
                    ThreadID = c.Int(nullable: false),

                })
                .PrimaryKey(t => t.PostID)
                 .ForeignKey("dbo.Thread", t => t.ThreadID, cascadeDelete: true);

            CreateTable(
                "dbo.User",
                c => new
                {
                    UserID = c.Int(nullable: false),
                    UserRole = c.String(nullable: false),
                    Name = c.String(),
                    UserName = c.String(),
                    Password = c.String(),
                    NumberOfPosts = c.Int(),
                    CanEdit = c.Boolean(nullable: false),
                    CanDelete = c.Boolean(nullable: false),
                    CanPost = c.Boolean(nullable: false),
                    CreateThread = c.Boolean(nullable: false),
                    CreateClass = c.Boolean(nullable: false),
                    CanPromote = c.Boolean(nullable: false),
                    ClassID = c.Int(nullable: false)
                })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Class", t => t.ClassID, cascadeDelete: true);

        }

        public override void Down()
        {
        }
    }
}
