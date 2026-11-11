using System.ComponentModel.DataAnnotations;

namespace SEWebApp.Models
{
    public class RecallNotice
    {
        [Key]
        public string RECALL_NUMBER { get; set; } = "";

        public string PRODUCT_CODE { get; set; } = "";
        public string PRODUCT_CODE_NAME { get; set; } = "";
        public int REGULATION_NUMBER { get; set; } = 0;
        public string PRODUCT_DESCRIPTION { get; set; } = "";
        public string TRADE_NAME { get; set; } = "";
        public string RECALL_CLASS { get; set; } = "";
        public string CENTER_CLASSIFICATION_DT { get; set; } = "";
        public string POSTED_INTERNET_DT { get; set; } = "";
        public string TERMINATION_DT { get; set; } = "";
        public string FEI_NUMBER { get; set; } = "";
        public string FIRM_NAME { get; set; } = "";
        public string MANUFACTURER_RECALL_REASON { get; set; } = "";
    }
}