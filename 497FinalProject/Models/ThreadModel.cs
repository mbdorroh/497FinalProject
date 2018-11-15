using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _497FinalProject.Models
{
    public class PublicClassThreadModel
    {
        public string ThreadName { get; set; }
        public string ThreadCategory { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateOfLastPost { get; set; }
        public int NoOfPosts { get; set; }
        public int ThreadID { get; set; }
    }
}