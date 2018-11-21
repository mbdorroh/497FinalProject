﻿using System;
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
        public string ThreadName { get; set; }
        [Display(Name = "Thread Category")]
        public string ThreadCategory { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateOfLastPost { get; set; }
        public int NoOfPosts { get; set; }
        [Key]
        public int ThreadID { get; set; }
        [Display(Name = "Class ID")]
        public int ClassID { get; set; }
    }
}