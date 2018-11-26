namespace _497FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClassModels", "NoOfThreads", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClassModels", "NoOfThreads");
        }
    }
}
