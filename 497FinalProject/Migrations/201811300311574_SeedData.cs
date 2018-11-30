namespace _497FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedData : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [NumberOfPosts], [CanEdit], [CanDelete], [CanPost], [CreateThread], [CreateClass], [CanPromote]) VALUES (N'df1d6e28-6af4-4e57-ab99-7491f1c7121c', N'ta@misforum.com', 0, N'AB1vscl9NdenBUgQm0OM1cJaOj+D4PutVmeP0ekbGSBXJYFVRm/hZq54z+OLFS95+g==', N'4a125396-41b3-4103-b2ae-4233d0f11bc8', NULL, 0, 0, NULL, 0, 0, N'ta@misforum.com', N'Teaching', N'Assistant', 0, 0, 0, 0, 0, 0, 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [NumberOfPosts], [CanEdit], [CanDelete], [CanPost], [CreateThread], [CreateClass], [CanPromote]) VALUES (N'2b629475-49b8-45ba-bf95-19fffc65a204', N'student@misforum.com', 0, N'AKZTFA+AqeKxm7LcLzBi/w73Dg5GZ7Q07YM40MDQ/GM1QJtbjZ55tMh7dninjGQL1w==', N'e2b9adf6-a03b-4a94-a6af-ba622f6b8e7e', NULL, 0, 0, NULL, 1, 0, N'student@misforum.com', N'Student', N'User', 0, 0, 0, 0, 0, 0, 0)

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'fd917952-5c37-41ce-895f-af864ed289a2', N'Student')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'65b86aa5-287a-4867-8d97-14a53acdff2e', N'TA')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'df1d6e28-6af4-4e57-ab99-7491f1c7121c', N'65b86aa5-287a-4867-8d97-14a53acdff2e')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2b629475-49b8-45ba-bf95-19fffc65a204', N'fd917952-5c37-41ce-895f-af864ed289a2')

SET IDENTITY_INSERT [dbo].[ClassModels] ON
INSERT INTO [dbo].[ClassModels] ([ClassID], [ClassName], [ProfessorID], [NoOfThreads]) VALUES (7, N'MIS 200', 0, 2)
INSERT INTO [dbo].[ClassModels] ([ClassID], [ClassName], [ProfessorID], [NoOfThreads]) VALUES (8, N'MIS 220', 0, 1)
INSERT INTO [dbo].[ClassModels] ([ClassID], [ClassName], [ProfessorID], [NoOfThreads]) VALUES (9, N'MIS 320', 0, 2)
SET IDENTITY_INSERT [dbo].[ClassModels] OFF

SET IDENTITY_INSERT [dbo].[ThreadModels] ON
INSERT INTO [dbo].[ThreadModels] ([ThreadID], [ThreadName], [ThreadCategory], [DateCreated], [DateOfLastPost], [NoOfPosts], [ClassID]) VALUES (8, N'Office Hours', N'General Class Info', N'2018-11-29 20:22:53', N'2018-11-29 20:45:33', 2, 7)
INSERT INTO [dbo].[ThreadModels] ([ThreadID], [ThreadName], [ThreadCategory], [DateCreated], [DateOfLastPost], [NoOfPosts], [ClassID]) VALUES (9, N'Assignment 7', N'How to do #3 on Assignment 7', N'2018-11-29 20:28:57', N'2018-11-29 20:56:17', 1, 8)
INSERT INTO [dbo].[ThreadModels] ([ThreadID], [ThreadName], [ThreadCategory], [DateCreated], [DateOfLastPost], [NoOfPosts], [ClassID]) VALUES (10, N'Exam Help', N'Exam 1 Practice Questions', N'2018-11-29 20:29:19', N'2018-11-29 20:58:36', 2, 9)
INSERT INTO [dbo].[ThreadModels] ([ThreadID], [ThreadName], [ThreadCategory], [DateCreated], [DateOfLastPost], [NoOfPosts], [ClassID]) VALUES (11, N'Class Attendance', N'Class grade based on attendance', N'2018-11-29 20:32:14', N'2018-11-29 20:54:37', 1, 7)
INSERT INTO [dbo].[ThreadModels] ([ThreadID], [ThreadName], [ThreadCategory], [DateCreated], [DateOfLastPost], [NoOfPosts], [ClassID]) VALUES (12, N'AIMS Attendance', N'alternative AIMS credits', N'2018-11-29 20:32:40', N'2018-11-29 20:59:22', 1, 9)
SET IDENTITY_INSERT [dbo].[ThreadModels] OFF


SET IDENTITY_INSERT [dbo].[PostModels] ON
INSERT INTO [dbo].[PostModels] ([PostID], [Subject], [PostBody], [PostUserName], [PostUserType], [TimePost], [Approval], [Disapproval], [isSolution], [ThreadID], [Comment], [ApplicationUser_Id]) VALUES (5, N'Office Hours', N'Are office hours be cancelled today?', N'student@misforum.com', NULL, N'2018-11-29 20:44:11', 0, 0, 0, 8, NULL, NULL)
INSERT INTO [dbo].[PostModels] ([PostID], [Subject], [PostBody], [PostUserName], [PostUserType], [TimePost], [Approval], [Disapproval], [isSolution], [ThreadID], [Comment], [ApplicationUser_Id]) VALUES (6, N'Office Hours', N'Office hours have not changed for today', N'ta@misforum.com', NULL, N'2018-11-29 20:45:33', 0, 0, 0, 8, NULL, NULL)
INSERT INTO [dbo].[PostModels] ([PostID], [Subject], [PostBody], [PostUserName], [PostUserType], [TimePost], [Approval], [Disapproval], [isSolution], [ThreadID], [Comment], [ApplicationUser_Id]) VALUES (7, N'Class Attendance Grade', N'How many attendance drops do we get?', N'student@misforum.com', NULL, N'2018-11-29 20:54:37', 0, 0, 0, 11, NULL, NULL)
INSERT INTO [dbo].[PostModels] ([PostID], [Subject], [PostBody], [PostUserName], [PostUserType], [TimePost], [Approval], [Disapproval], [isSolution], [ThreadID], [Comment], [ApplicationUser_Id]) VALUES (8, N'Assignment 7', N'#3 on assignment 7 has been removed', N'ta@misforum.com', NULL, N'2018-11-29 20:56:17', 0, 0, 0, 9, NULL, NULL)
INSERT INTO [dbo].[PostModels] ([PostID], [Subject], [PostBody], [PostUserName], [PostUserType], [TimePost], [Approval], [Disapproval], [isSolution], [ThreadID], [Comment], [ApplicationUser_Id]) VALUES (9, N'Practice Questions', N'When will the practice questions be posted?', N'student@misforum.com', NULL, N'2018-11-29 20:56:48', 0, 0, 0, 10, NULL, NULL)
INSERT INTO [dbo].[PostModels] ([PostID], [Subject], [PostBody], [PostUserName], [PostUserType], [TimePost], [Approval], [Disapproval], [isSolution], [ThreadID], [Comment], [ApplicationUser_Id]) VALUES (10, N'Exam 1 Practice Questions', N'Exam 1 practice questions will be posted next Tuesday.', N'ta@misforum.com', NULL, N'2018-11-29 20:58:36', 0, 0, 0, 10, NULL, NULL)
INSERT INTO [dbo].[PostModels] ([PostID], [Subject], [PostBody], [PostUserName], [PostUserType], [TimePost], [Approval], [Disapproval], [isSolution], [ThreadID], [Comment], [ApplicationUser_Id]) VALUES (11, N'AIMS Credit', N'WIT, CMISS, and VIB count as alternative AIMS credits', N'admin@misforum.com', NULL, N'2018-11-29 20:59:22', 0, 0, 0, 12, NULL, NULL)
SET IDENTITY_INSERT [dbo].[PostModels] OFF

               
            ");
        }
        
        public override void Down()
        {
        }
    }
}
