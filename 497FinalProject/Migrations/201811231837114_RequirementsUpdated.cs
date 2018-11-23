namespace _497FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequirementsUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClassModels", "Professor_UserID", "dbo.UserModels");
            DropIndex("dbo.ClassModels", new[] { "Professor_UserID" });
            AlterColumn("dbo.ClassModels", "ClassName", c => c.String(nullable: false));
            AlterColumn("dbo.ClassModels", "Professor_UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.ThreadModels", "ThreadName", c => c.String(nullable: false));
            AlterColumn("dbo.ThreadModels", "ThreadCategory", c => c.String(nullable: false));
            CreateIndex("dbo.ClassModels", "Professor_UserID");
            AddForeignKey("dbo.ClassModels", "Professor_UserID", "dbo.UserModels", "UserID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassModels", "Professor_UserID", "dbo.UserModels");
            DropIndex("dbo.ClassModels", new[] { "Professor_UserID" });
            AlterColumn("dbo.ThreadModels", "ThreadCategory", c => c.String());
            AlterColumn("dbo.ThreadModels", "ThreadName", c => c.String());
            AlterColumn("dbo.ClassModels", "Professor_UserID", c => c.Int());
            AlterColumn("dbo.ClassModels", "ClassName", c => c.String());
            CreateIndex("dbo.ClassModels", "Professor_UserID");
            AddForeignKey("dbo.ClassModels", "Professor_UserID", "dbo.UserModels", "UserID");
        }
    }
}
