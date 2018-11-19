using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _497FinalProject.Models
{
    public class ClassModel
    {
        //class attributes for class model
        [Display(Name = "Class ID")]
        public int ClassID { get; set; }
        [Display(Name = "Class Name")]
        public string ClassName { get; set; }
        [Display(Name = "User ID")]
        public UserModel Professor { get; set; }
        public List<ThreadModel> Threads;

        public IEnumerator<ClassModel> GetEnumerator()
        {
            return GetEnumerator();
        }
        
    }
}