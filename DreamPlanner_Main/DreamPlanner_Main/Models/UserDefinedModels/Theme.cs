using DreamPlanner_Main.Models.UserDefinedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DreamPlanner_Main.Models.UserDefinedModels
{
    public class Theme
    {
        public int ThemeId { get; set; }
        public string ThemeName { get; set; }

        public int PartyTypeId { get; set; }
        public PartyType PartyType { get; set; }
        
        public List<Reservation> Reservations { get; set; }
    }
}