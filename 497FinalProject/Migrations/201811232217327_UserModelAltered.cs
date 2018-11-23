namespace _497FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserModelAltered : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserModels", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.UserModels", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.UserModels", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.UserModels", "UserRole", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserModels", "UserRole", c => c.String());
            AlterColumn("dbo.UserModels", "Password", c => c.String());
            AlterColumn("dbo.UserModels", "UserName", c => c.String());
            AlterColumn("dbo.UserModels", "Name", c => c.String());
        }
    }
}
