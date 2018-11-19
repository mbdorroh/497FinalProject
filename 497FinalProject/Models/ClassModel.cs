using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _497FinalProject.Models
{
    public class ClassModel
    {
        //class attributes for class model
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public UserModel Professor { get; set; }
        public List<ThreadModel> Threads;
    }
}