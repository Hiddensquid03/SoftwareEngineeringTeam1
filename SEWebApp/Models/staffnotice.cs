namespace StaffRecallApp.Models
{
    // One notice shown in the "Staff Notices" list
    public class StaffNotice
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateIssued { get; set; }
    }
}
