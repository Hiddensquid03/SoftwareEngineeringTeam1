
using Microsoft.EntityFrameworkCore;
using SEWebApp.Models;

namespace SEWebApp.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<RecallNotice> RecallNotices { get; set; }

        public DbSet<StaffReport> StaffReports { get; set; }
    }
}
