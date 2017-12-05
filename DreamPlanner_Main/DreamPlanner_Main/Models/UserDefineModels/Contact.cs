using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DreamPlanner_Main.Models.UserDefineModels
{
    public class Contact
    {
        public int ContactID { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string message { get; set; }
        public bool ContactOrNot { get; set; }
    }
}