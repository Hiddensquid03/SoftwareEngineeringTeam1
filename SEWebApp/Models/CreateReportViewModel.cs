namespace SEWebApp.Models
{
    public class CreateReportViewModel
    {
        public int ReportID { get; set; }
        public string RECALL_NUMBER { get; set; } = "";
        public string FIRM_NAME { get; set; } = "";
        public string TRADE_NAME { get; set; } = "";
        public string PRODUCT_DESCRIPTION { get; set; } = "";
        public string MANUFACTURER_RECALL_REASON { get; set; } = "";
        public string ReportText { get; set; } = "";
    }
}
