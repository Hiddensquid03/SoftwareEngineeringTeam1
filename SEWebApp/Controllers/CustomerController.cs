
using Microsoft.AspNetCore.Mvc;
using SEWebApp.Models;

namespace SEWebApp.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index(
            string? search,
            string sort = "newest")
        {
            // Temporary notices for testing.
            // These represent notices shared with a customer.
            var notices = new List<CustomerNoticeViewModel>
            {
                new CustomerNoticeViewModel
                {
                    Id = 1,
                    Title = "Infusion Pump Recall",
                    Manufacturer = "Example Medical",
                    NoticeDate = new DateTime(2026, 10, 20),
                    CommentCount = 5,
                    Description = "Pump safety recall notice."
                },

                new CustomerNoticeViewModel
                {
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

            // Sort notices based on the selected option.
            notices = sort.ToLowerInvariant() switch
            {
                "oldest" => notices
                    .OrderBy(n => n.NoticeDate)
                    .ToList(),

                "comments" => notices
                    .OrderByDescending(n => n.CommentCount)
                    .ToList(),

                "alphabetical" => notices
                    .OrderBy(n => n.Title)
                    .ToList(),

                _ => notices
                    .OrderByDescending(n => n.NoticeDate)
                    .ToList()
            };

            // Keep selected values after submitting.
            ViewBag.Search = search;
            ViewBag.Sort = sort;

            return View(notices);
        }
    }
}
