namespace _497FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MinorKeyEdits : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClassModels", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.PostModels", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ClassModels", "ApplicationUser_Id");
            CreateIndex("dbo.PostModels", "ThreadID");
            CreateIndex("dbo.PostModels", "ApplicationUser_Id");
            AddForeignKey("dbo.PostModels", "ThreadID", "dbo.ThreadModels", "ThreadID", cascadeDelete: true);
            AddForeignKey("dbo.ClassModels", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PostModels", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClassModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostModels", "ThreadID", "dbo.ThreadModels");
            DropIndex("dbo.PostModels", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.PostModels", new[] { "ThreadID" });
            DropIndex("dbo.ClassModels", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.PostModels", "ApplicationUser_Id");
            DropColumn("dbo.ClassModels", "ApplicationUser_Id");
        }
    }
}
