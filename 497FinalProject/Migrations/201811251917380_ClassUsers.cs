namespace _497FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserModels", "ClassModel_ClassID", c => c.Int());
            CreateIndex("dbo.UserModels", "ClassModel_ClassID");
            AddForeignKey("dbo.UserModels", "ClassModel_ClassID", "dbo.ClassModels", "ClassID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserModels", "ClassModel_ClassID", "dbo.ClassModels");
            DropIndex("dbo.UserModels", new[] { "ClassModel_ClassID" });
            DropColumn("dbo.UserModels", "ClassModel_ClassID");
        }
    }
}
