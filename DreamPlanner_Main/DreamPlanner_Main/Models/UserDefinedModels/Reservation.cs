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
        [DisplayName("Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReservationDate
        {
            get { return (defaultDate == DateTime.MinValue) ? DateTime.Now : defaultDate; }
            set { defaultDate = value; }
        }

        [Required]
        public int TotalRent { get; set; }

        public string ReservationCode { get; set; }

        public bool AdvancePayment { get; set; }

        public bool IsPaid { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}