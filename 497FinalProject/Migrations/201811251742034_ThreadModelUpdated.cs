namespace _497FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThreadModelUpdated : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ThreadModels", "ClassID");
            AddForeignKey("dbo.ThreadModels", "ClassID", "dbo.ClassModels", "ClassID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThreadModels", "ClassID", "dbo.ClassModels");
            DropIndex("dbo.ThreadModels", new[] { "ClassID" });
        }
    }
}
