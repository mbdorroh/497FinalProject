using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _497FinalProject.Models
{
    public class PostModel
    {
        //post attributes for post model
        public string Subject { get; set; }
        public string PostBody { get; set; }
        public string PostUserName { get; set; }
        public string PostUserType { get; set; }
        public DateTime TimePost { get; set; }
        public int Approval { get; set; }
        public int Disapproval { get; set; }
        public bool isSolution { get; set; }
        public int ThreadID { get; set; }
        public string Comment { get; set; }
        public int PostID { get; set; }
    }
}