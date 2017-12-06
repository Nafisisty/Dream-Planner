using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DreamPlanner_Main.Models.UserDefinedModels
{
    public class PartyType
    {
        public int PartyTypeId { get; set; }
        public string PartyTypeName { get; set; }

        public List<Theme> Themes { get; set; }
    }
}