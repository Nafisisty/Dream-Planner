using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DreamPlanner_Main.Models.UserDefinedModels
{
    public class Hall
    {
        public int HallId { get; set; }
        [Required]
        [DisplayName("Name")]
        public string HallName { get; set; }
        [Required]
        [DisplayName("Capacity")]
        public int HallCapacity { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}