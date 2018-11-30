namespace _497FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Jim : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserModels", "ClassModel_ClassID", "dbo.ClassModels");
            DropForeignKey("dbo.ClassModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ClassModels", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserModels", new[] { "ClassModel_ClassID" });
            CreateTable(
                "dbo.ApplicationUserClassModels",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        ClassModel_ClassID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.ClassModel_ClassID })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.ClassModels", t => t.ClassModel_ClassID, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ClassModel_ClassID);
            
            DropColumn("dbo.ClassModels", "ApplicationUser_Id");
            DropColumn("dbo.UserModels", "ClassModel_ClassID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserModels", "ClassModel_ClassID", c => c.Int());
            AddColumn("dbo.ClassModels", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.ApplicationUserClassModels", "ClassModel_ClassID", "dbo.ClassModels");
            DropForeignKey("dbo.ApplicationUserClassModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserClassModels", new[] { "ClassModel_ClassID" });
            DropIndex("dbo.ApplicationUserClassModels", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserClassModels");
            CreateIndex("dbo.UserModels", "ClassModel_ClassID");
            CreateIndex("dbo.ClassModels", "ApplicationUser_Id");
            AddForeignKey("dbo.ClassModels", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.UserModels", "ClassModel_ClassID", "dbo.ClassModels", "ClassID");
        }
    }
}
