using Microsoft.AspNetCore.Mvc;
using StaffRecallApp.Models;

namespace StaffRecallApp.Controllers
{
    public class StaffController : Controller
    {
        // Temporary in-memory data; will be replaced by the database later
        private static readonly List<StaffNotice> _notices = new List<StaffNotice>
        {
            new StaffNotice { Id = 1, Title = "Batch 204 recall issued", Description = "Check stock and contact affected clients.", DateIssued = new DateTime(2026, 11, 2) },
            new StaffNotice { Id = 2, Title = "Updated recall procedure", Description = "New steps for logging client responses.", DateIssued = new DateTime(2026, 11, 4) },
            new StaffNotice { Id = 3, Title = "System maintenance on Friday", Description = "Recall database unavailable 18:00 to 20:00.", DateIssued = new DateTime(2026, 11, 6) },
            new StaffNotice { Id = 4, Title = "Client list review", Description = "Please verify contact details are current.", DateIssued = new DateTime(2026, 11, 9) },
            new StaffNotice { Id = 5, Title = "Training session reminder", Description = "Recall handling training, Monday 10:00.", DateIssued = new DateTime(2026, 11, 10) }
        };

        // GET: /Staff or /Staff?searchTerm=batch
        public IActionResult Index(string? searchTerm)
        {
            var results = _notices.AsEnumerable();

            // Filter notices by title or description when a search term is entered
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                results = results.Where(n =>
                    n.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    n.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            var model = new StaffListViewModel
            {
                LoggedInUser = "Staff", // placeholder until login is built
                SearchTerm = searchTerm,
                Notices = results.OrderByDescending(n => n.DateIssued).ToList()
            };

            return View(model);
        }
    }
}

