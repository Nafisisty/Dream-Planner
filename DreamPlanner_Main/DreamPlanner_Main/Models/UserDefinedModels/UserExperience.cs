using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DreamPlanner_Main.Models.UserDefinedModels
{
    public class UserExperience
    {
        public int UserExperienceId { get; set; }

        [Required]
        [DisplayName("Rating")]
        public int RatingId { get; set; }
        public Rating Rating { get; set; }

        [Required]
        [DisplayName("Why have choosen this rating?")]
        [DataType(DataType.MultilineText)]
        public string ExperienceDescription { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}