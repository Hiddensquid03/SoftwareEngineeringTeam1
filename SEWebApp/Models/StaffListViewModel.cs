namespace StaffRecallApp.Models
{
    // Data the Staff List view needs: the logged-in user, the search text and the notices
    public class StaffListViewModel
    {
        public string LoggedInUser { get; set; } = string.Empty;
        public string? SearchTerm { get; set; }
        public List<StaffNotice> Notices { get; set; } = new List<StaffNotice>();
    }
}
