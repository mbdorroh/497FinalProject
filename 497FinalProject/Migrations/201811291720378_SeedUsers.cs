namespace _497FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [NumberOfPosts], [CanEdit], [CanDelete], [CanPost], [CreateThread], [CreateClass], [CanPromote]) VALUES (N'0e6a2cf6-b793-4a0c-be78-d1bbe449f22d', N'admin@misforum.com', 0, N'AA3TNxTRhULbCvuVjVnqjH78jMfjE1ugGM5yf1vm0thUzHYooKtq2Dcz0z76uRGoVg==', N'a1de6ef5-b01d-4b0c-8730-1959e299a0c6', NULL, 0, 0, NULL, 1, 0, N'admin@misforum.com', N'Admin', N'User', 0, 0, 0, 0, 0, 0, 0)
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'c49547a3-7f65-4142-aa45-4ed1068b5368', N'Professor')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0e6a2cf6-b793-4a0c-be78-d1bbe449f22d', N'c49547a3-7f65-4142-aa45-4ed1068b5368')

");
        }
        
        public override void Down()
        {
        }
    }
}
