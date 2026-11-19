
using System;
using System.ComponentModel.DataAnnotations;

namespace SEWebApp.Models
{
    public class StaffReport
    {
        [Key]
        public int ReportID { get; set; }

        public string RECALL_NUMBER { get; set; } = "";

        public string ReportText { get; set; } = "";

        public string Status { get; set; } = "Draft";

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime LastModified { get; set; } = DateTime.Now;
    }
}
