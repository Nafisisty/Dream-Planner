using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DreamPlanner_Main.Models.UserDefinedModels
{
    public class Rating
    {
        public int RatingId { get; set; }
        public string RatingName { get; set; }

        public List<UserExperience> UserExperiences { get; set; }
    }
}