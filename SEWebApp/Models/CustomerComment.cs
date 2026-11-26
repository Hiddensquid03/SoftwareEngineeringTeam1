
using System;
using System.ComponentModel.DataAnnotations;

namespace SEWebApp.Models
{
    public class CustomerComment
    {
        [Key]
        public int CustomerCommentId { get; set; }

        public int StaffReportId { get; set; }

        [Required]
        [StringLength(2000)]
        public string CommentText { get; set; } = "";

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;
    }
}
