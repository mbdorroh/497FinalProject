namespace _497FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassAltered : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClassModels", "Professor_UserID", "dbo.UserModels");
            DropIndex("dbo.ClassModels", new[] { "Professor_UserID" });
            AddColumn("dbo.ClassModels", "ProfessorID", c => c.Int(nullable: false));
            DropColumn("dbo.ClassModels", "Professor_UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClassModels", "Professor_UserID", c => c.Int(nullable: false));
            DropColumn("dbo.ClassModels", "ProfessorID");
            CreateIndex("dbo.ClassModels", "Professor_UserID");
            AddForeignKey("dbo.ClassModels", "Professor_UserID", "dbo.UserModels", "UserID", cascadeDelete: true);
        }
    }
}
