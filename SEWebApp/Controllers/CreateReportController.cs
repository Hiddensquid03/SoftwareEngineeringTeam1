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

        //Displaying the Create Report page
        [HttpGet]
        public async Task<IActionResult> Index(string recallNumber)
        {
            if (string.IsNullOrEmpty(recallNumber))
            {
                return BadRequest("A Valid Recall Number is required.");
            }

            //Finding the Recall Notice in the database
            var recallNotice = await _context.RecallNotices
                .FirstOrDefaultAsync(r => r.RecallNumber == recallNumber);
            
            if (recallNotice == null)
            {
                return NotFound("Recall Notice not found.");
            }

            //Transferring the data from the Recall Notice to the CreateReportViewModel
            var createReportViewModel = new CreateReportViewModel
            {
                RecallNumber = recallNotice.RecallNumber,
                FirmName = recallNotice.FirmName,
                TradeName = recallNotice.TradeName,
                ProductDescription = recallNotice.ProductDescription,
                ManufacturerRecallReason = recallNotice.ManufacturerRecallReason
            };

            return View(createReportViewModel);
        }

        //Saving the report as a Draft
        [HttpPost]
        public IActionResult SaveDraft(CreateReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Logic to save the report as a draft
                // This could involve saving to a database or session
                TempData["DraftSaved"] = "Report Successfully Saved as Draft";
                return RedirectToAction("Index", new { recallNumber = model.RecallNumber });
            }
            // If the model state is invalid, return the same view with validation messages
            return View("Index", model);
        }
    }
}
