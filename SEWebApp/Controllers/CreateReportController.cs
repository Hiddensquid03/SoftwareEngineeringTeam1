
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEWebApp.Models;
using SEWebApp.Data;

namespace SEWebApp.Controllers
{
    public class CreateReportController : Controller
    {
        private readonly AppDBContext _context;

        public CreateReportController(AppDBContext context)
        {
            _context = context;
        }

        // Displaying the Create Report page
        [HttpGet]
        public async Task<IActionResult> Index(string recallNumber)
        {
            if (string.IsNullOrWhiteSpace(recallNumber))
            {
                return BadRequest("A valid Recall Number is required.");
            }

            var recallNotice = await _context.RecallNotices
                .FirstOrDefaultAsync(r => r.RECALL_NUMBER == recallNumber);

            if (recallNotice == null)
            {
                return NotFound("Recall Notice not found: " + recallNumber);
            }

            var model = new CreateReportViewModel
            {
                RECALL_NUMBER = recallNotice.RECALL_NUMBER,
                FIRM_NAME = recallNotice.FIRM_NAME,
                TRADE_NAME = recallNotice.TRADE_NAME,
                PRODUCT_DESCRIPTION = recallNotice.PRODUCT_DESCRIPTION,
                MANUFACTURER_RECALL_REASON = recallNotice.MANUFACTURER_RECALL_REASON
            };

            return View(model);
        }

        // Save report as a Draft
        [HttpPost]
        public async Task<IActionResult> SaveDraft(CreateReportViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.RECALL_NUMBER))
            {
                return BadRequest("Recall number was not received.");
            }

            var recallNotice = await _context.RecallNotices
                .FirstOrDefaultAsync(r => r.RECALL_NUMBER == model.RECALL_NUMBER);

            if (recallNotice == null)
            {
                return NotFound("Recall Notice not found: " + model.RECALL_NUMBER);
            }

            StaffReport report;

            if (model.ReportID > 0)
            {
                report = await _context.StaffReports
                    .FirstOrDefaultAsync(r => r.ReportID == model.ReportID);

                if (report == null)
                {
                    return NotFound("Report not found.");
                }
            }
            else
            {
                report = new StaffReport
                {
                    DateCreated = DateTime.Now
                };

                _context.StaffReports.Add(report);
            }

            report.RECALL_NUMBER = model.RECALL_NUMBER;
            report.ReportText = model.ReportText;
            report.Status = "Draft";
            report.LastModified = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction("EditDraft", new { reportID = report.ReportID });
        }

        // Open an existing Draft
        [HttpGet]
        public async Task<IActionResult> EditDraft(int reportID)
        {
            var report = await _context.StaffReports
                .FirstOrDefaultAsync(r => r.ReportID == reportID);

            if (report == null || report.Status != "Draft")
            {
                return NotFound("Draft not found.");
            }

            var recallNotice = await _context.RecallNotices
                .FirstOrDefaultAsync(r => r.RECALL_NUMBER == report.RECALL_NUMBER);

            if (recallNotice == null)
            {
                return NotFound("Recall Notice not found.");
            }

            var model = new CreateReportViewModel
            {
                ReportID = report.ReportID,
                RECALL_NUMBER = recallNotice.RECALL_NUMBER,
                FIRM_NAME = recallNotice.FIRM_NAME,
                TRADE_NAME = recallNotice.TRADE_NAME,
                PRODUCT_DESCRIPTION = recallNotice.PRODUCT_DESCRIPTION,
                MANUFACTURER_RECALL_REASON = recallNotice.MANUFACTURER_RECALL_REASON,
                ReportText = report.ReportText
            };

            return View("Index", model);
        }

        // Send report to customer
        [HttpPost]
        public async Task<IActionResult> SendToCustomer(CreateReportViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.RECALL_NUMBER))
            {
                return BadRequest("Recall number was not received.");
            }

            var recallNotice = await _context.RecallNotices
                .FirstOrDefaultAsync(r => r.RECALL_NUMBER == model.RECALL_NUMBER);

            if (recallNotice == null)
            {
                return NotFound("Recall Notice not found: " + model.RECALL_NUMBER);
            }

            StaffReport report;

            if (model.ReportID > 0)
            {
                report = await _context.StaffReports
                    .FirstOrDefaultAsync(r => r.ReportID == model.ReportID);

                if (report == null)
                {
                    return NotFound("Report not found.");
                }
            }
            else
            {
                report = new StaffReport
                {
                    DateCreated = DateTime.Now
                };

                _context.StaffReports.Add(report);
            }

            report.RECALL_NUMBER = model.RECALL_NUMBER;
            report.ReportText = model.ReportText;
            report.Status = "Sent";
            report.LastModified = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Staff");
        }
    }
}
