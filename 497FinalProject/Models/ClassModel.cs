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
        [Key]
        [Required]
        public int ClassID { get; set; }
        [Display(Name = "Class Name")]
        [Required]
        public string ClassName { get; set; }
        [Display(Name = "User ID")]
        [Required]
        public int ProfessorID { get; set; }
        public List<ThreadModel> Threads;

        public IEnumerator<ClassModel> GetEnumerator()
        {
            return GetEnumerator();
        }




    }
}