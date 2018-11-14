using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _497FinalProject.Models
{
    public class ClassModel
    {
        //class attributes
        public string ClassName { get; set; }
        public UserModel Professor { get; set; }
        //add ref to threads
    }
}