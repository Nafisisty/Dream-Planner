using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DreamPlanner_Main.Models.UserDefinedModels
{
    public class Reservation
    {
        private DateTime defaultDate = DateTime.Now;

        public int ReservationId { get; set; }

        [Required]
        [DisplayName("Choose Theme")]
        public int ThemeId { get; set; }
        public Theme Theme { get; set; }
        [Required]
        [DisplayName("Choose Hall")]
        public int HallId { get; set; }
        public Hall Hall { get; set; }
        [Required]
        [DisplayName("Select a date")]
        public DateTime ReservationDate { get; set; }

        [Required]
        public int TotalRent { get; set; }

        public string ReservationCode { get; set; }

        public bool AdvancePayment { get; set; }

        public bool IsPaid { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}