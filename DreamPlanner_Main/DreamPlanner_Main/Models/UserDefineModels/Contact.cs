using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DreamPlanner_Main.Models.UserDefineModels
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool ContactOrNot { get; set; }
    }
}