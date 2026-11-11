namespace SEWebApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using SEWebApp.Models;
    using System.Collections.Generic;

    public class AppDBContext : DBContext
    {
        // Database properties defined here
        // Just a placeholder for now, shows where CreateReportController.cs will pull the info from
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }
        public DbSet<CreateReportViewModel> RecallNotices { get; set; }
    }
}
