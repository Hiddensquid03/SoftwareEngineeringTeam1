namespace SEWebApp.Models
{
    public class CreateReportViewModel
    {
        public string RecallNumber { get; set; } = "";
        public string FirmName { get; set; } = "";
        public string TradeName { get; set; } = "";
        public string ProductDescription { get; set; } = "";
        public string ManufacturerRecallReason { get; set; } = "";
        public string ReportText { get; set; } = "";
    }
}
