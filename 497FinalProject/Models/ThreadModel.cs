using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _497FinalProject.Models
{
    public class ThreadModel
    {
        //attributes for thread model
        [Display(Name = "Thread Name")]
        [Required]
        public string ThreadName { get; set; }
        [Display(Name = "Thread Category")]
        [Required]
        public string ThreadCategory { get; set; }
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Date of Last Post")]
        public DateTime DateOfLastPost { get; set; }
        [Display(Name = "No. of Posts")]
        public int NoOfPosts { get; set; }
        [Key]
        [Display(Name = "Thread ID")]
        public int ThreadID { get; set; }
        [Display(Name = "Class ID")]
        public int ClassID { get; set; }
        public ICollection<PostModel> Posts { get; set; }
    }
}