
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEWebApp.Data;

namespace SEWebApp.Controllers
{
    public class StaffController : Controller
    {
        private readonly AppDBContext _context;

        public StaffController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            var notices = _context.RecallNotices.AsQueryable();

            switch (sortOrder)
            {
                case "product_asc":
                    notices = notices.OrderBy(n => n.PRODUCT_CODE);
                    break;

                case "product_desc":
                    notices = notices.OrderByDescending(n => n.PRODUCT_CODE);
                    break;
                        
                case "date_asc":
                    notices = notices.OrderBy(n =>
                        n.TERMINATION_DT.Substring(
                            n.TERMINATION_DT.Length - 4, 4));
                    break;

                case "date_desc":
                    notices = notices.OrderByDescending(n =>
                        n.TERMINATION_DT.Substring(
                            n.TERMINATION_DT.Length - 4, 4));
                    break;

                default:
                    notices = notices.OrderBy(n => n.PRODUCT_CODE);
                    sortOrder = "product_asc";
                    break;
            }

            ViewBag.SortOrder = sortOrder;

            return View(await notices.ToListAsync());
        }
    }
}
