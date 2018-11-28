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
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public int NumberOfPosts { get; set; }
        [Required]
        [Display(Name = "User Role")]
        public string UserRole { get; set; }
        [Key]
        [Display(Name = "User ID")]
        public int UserID { get; set; }
        public ICollection<ClassModel> Classes;

        //user permissions
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanPost { get; set; }
        public bool CreateThread { get; set; }
        public bool CreateClass { get; set; }
        public bool CanPromote { get; set; }

        
    }
}