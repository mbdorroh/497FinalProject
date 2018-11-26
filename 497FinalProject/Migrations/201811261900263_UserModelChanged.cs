namespace _497FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserModelChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "NumberOfPosts", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "CanEdit", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "CanDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "CanPost", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "CreateThread", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "CreateClass", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "CanPromote", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CanPromote");
            DropColumn("dbo.AspNetUsers", "CreateClass");
            DropColumn("dbo.AspNetUsers", "CreateThread");
            DropColumn("dbo.AspNetUsers", "CanPost");
            DropColumn("dbo.AspNetUsers", "CanDelete");
            DropColumn("dbo.AspNetUsers", "CanEdit");
            DropColumn("dbo.AspNetUsers", "NumberOfPosts");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
