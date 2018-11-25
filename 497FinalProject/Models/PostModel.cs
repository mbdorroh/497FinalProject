using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _497FinalProject.Models
{
    public class PostModel
    {
        //post attributes for post model
        [StringLength(256, MinimumLength = 10, ErrorMessage = "field must be at least 10 characters")]
        public string Subject { get; set; }
        [Display(Name = "Post Body")]
        [StringLength(500, MinimumLength = 25, ErrorMessage = "field must be at least 25 characters")]
        public string PostBody { get; set; }
        [Display(Name = "Username")]
        public string PostUserName { get; set; }
        public string PostUserType { get; set; }
        [Display(Name = "Time Posted")]
        public DateTime TimePost { get; set; }
        public int Approval { get; set; }
        public int Disapproval { get; set; }
        public bool isSolution { get; set; }
        public int ThreadID { get; set; }
        public string Comment { get; set; }
        [Key]
        public int PostID { get; set; }
    }
}