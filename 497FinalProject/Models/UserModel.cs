using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _497FinalProject.Models
{
    public class UserModel
    {
        //user details for user model
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int NumberOfPosts { get; set; }
        public string UserRole { get; set; }
        public int UserID { get; set; }
        public List<ClassModel> Classes;

        //user permissions
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanPost { get; set; }
        public bool CreateThread { get; set; }
        public bool CreateClass { get; set; }
        public bool CanPromote { get; set; }

    }
}