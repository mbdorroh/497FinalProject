namespace _497FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostModelEdit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PostModels", "Subject", c => c.String(maxLength: 256));
            AlterColumn("dbo.PostModels", "PostBody", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PostModels", "PostBody", c => c.String());
            AlterColumn("dbo.PostModels", "Subject", c => c.String());
        }
    }
}
