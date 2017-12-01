using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DreamPlanner_Main.Models.UserDefinedModels
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Please enter a valid email address")]
        public string UserEmail { get; set; }
        [Required]
        public int CityId { get; set; }
        public City City { get; set; }
        [Required]
        [DisplayName("Street Address")]
        public string UserStreetAddress { get; set; }
        [Required]
        [DisplayName("Username")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Password")]
        public string UserPassword { get; set; }
    }
}