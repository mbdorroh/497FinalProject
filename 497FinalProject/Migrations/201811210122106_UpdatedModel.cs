namespace _497FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassModels",
                c => new
                    {
                        ClassID = c.Int(nullable: false, identity: true),
                        ClassName = c.String(),
                        Professor_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ClassID)
                .ForeignKey("dbo.UserModels", t => t.Professor_UserID)
                .Index(t => t.Professor_UserID);
            
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        NumberOfPosts = c.Int(nullable: false),
                        UserRole = c.String(),
                        CanEdit = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        CanPost = c.Boolean(nullable: false),
                        CreateThread = c.Boolean(nullable: false),
                        CreateClass = c.Boolean(nullable: false),
                        CanPromote = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.PostModels",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        PostBody = c.String(),
                        PostUserName = c.String(),
                        PostUserType = c.String(),
                        TimePost = c.DateTime(nullable: false),
                        Approval = c.Int(nullable: false),
                        Disapproval = c.Int(nullable: false),
                        isSolution = c.Boolean(nullable: false),
                        ThreadID = c.Int(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.PostID);
            
            CreateTable(
                "dbo.ThreadModels",
                c => new
                    {
                        ThreadID = c.Int(nullable: false, identity: true),
                        ThreadName = c.String(),
                        ThreadCategory = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateOfLastPost = c.DateTime(nullable: false),
                        NoOfPosts = c.Int(nullable: false),
                        ClassID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ThreadID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassModels", "Professor_UserID", "dbo.UserModels");
            DropIndex("dbo.ClassModels", new[] { "Professor_UserID" });
            DropTable("dbo.ThreadModels");
            DropTable("dbo.PostModels");
            DropTable("dbo.UserModels");
            DropTable("dbo.ClassModels");
        }
    }
}
