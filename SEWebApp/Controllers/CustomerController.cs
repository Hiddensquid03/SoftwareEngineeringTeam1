
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEWebApp.Data;
using SEWebApp.Models;

namespace SEWebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDBContext _context;

        public CustomerController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(
            string? search, string sort = "newest")
        {
            // Retrieve reports marked Sent from SQL Server.
            // TEMPORARY integration test only:
            // customer-specific access is not implemented yet.
            var reports = await _context.StaffReports
                .AsNoTracking()
                .Where(r => r.Status == "Sent")
                .ToListAsync();

            var recalls = await _context.RecallNotices
                .AsNoTracking()
                .ToListAsync();

            var notices = reports.Select(report =>
            {
                var recall = recalls.FirstOrDefault(r =>
                    r.RECALL_NUMBER == report.RECALL_NUMBER);

                return new CustomerNoticeViewModel
                {
<<<<<<< Updated upstream
                    Id = 2,
                    Title = "Patient Monitor Recall",
                    Manufacturer = "Example Healthcare",
                    NoticeDate = new DateTime(2026, 9, 15),
                    CommentCount = 12,
                    Description = "Patient monitor safety notice."
                },
                
                new CustomerNoticeViewModel
                {
                    Id = 3,
                    Title = "Ventilator Product Recall",
                    Manufacturer = "Example Devices",
                    NoticeDate = new DateTime(2026, 8, 2),
                    CommentCount = 3,
                    Description = "Ventilator product recall notice."
                }
            };

            // Search by notice title or manufacturer.
=======
                    Id = report.ReportID,
                    RecallNumber = report.RECALL_NUMBER,
                    Title = recall?.TRADE_NAME
                        ?? report.RECALL_NUMBER,
                    Manufacturer = recall?.FIRM_NAME
                        ?? "Manufacturer unavailable",
                    NoticeDate = report.DateCreated,
                    CommentCount = 0,
                    Description = recall?.PRODUCT_DESCRIPTION
                        ?? "Device information unavailable",
                    ReportText = report.ReportText
                };
            }).ToList();

>>>>>>> Stashed changes
            if (!string.IsNullOrWhiteSpace(search))
            {
                notices = notices.Where(n =>
                    n.Title.Contains(
                        search,
                        StringComparison.OrdinalIgnoreCase)
                    ||
                    n.Manufacturer.Contains(
                        search,
                        StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            notices = sort.ToLowerInvariant() switch
            {
                "oldest" => notices
                    .OrderBy(n => n.NoticeDate).ToList(),

                "comments" => notices
                    .OrderByDescending(n => n.CommentCount).ToList(),

                "alphabetical" => notices
                    .OrderBy(n => n.Title).ToList(),

                _ => notices
                    .OrderByDescending(n => n.NoticeDate).ToList()
            };

            ViewBag.Search = search;
            ViewBag.Sort = sort;

            return View(notices);
        }
<<<<<<< Updated upstream
=======

        public async Task<IActionResult> Details(int id)
        {
            var report = await _context.StaffReports
                .AsNoTracking()
                .FirstOrDefaultAsync(r =>
                    r.ReportID == id && r.Status == "Sent");

            if (report == null)
                return NotFound();

            var recall = await _context.RecallNotices
                .AsNoTracking()
                .FirstOrDefaultAsync(r =>
                    r.RECALL_NUMBER == report.RECALL_NUMBER);

            var device = new CustomerNoticeViewModel
            {
                Id = report.ReportID,
                RecallNumber = report.RECALL_NUMBER,
                Title = recall?.TRADE_NAME
                    ?? report.RECALL_NUMBER,
                Manufacturer = recall?.FIRM_NAME
                    ?? "Manufacturer unavailable",
                NoticeDate = report.DateCreated,
                CommentCount = 0,
                Description = recall?.PRODUCT_DESCRIPTION
                    ?? "Device information unavailable",
                ReportText = report.ReportText
            };

            return View(device);
        }
>>>>>>> Stashed changes
    }
}
