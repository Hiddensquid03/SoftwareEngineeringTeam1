
namespace SEWebApp.Models
{
    public class CustomerNoticeViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Manufacturer { get; set; } = "";

        public DateTime NoticeDate { get; set; }

        public int CommentCount { get; set; }

        public string Description { get; set; } = "";
    }
}
