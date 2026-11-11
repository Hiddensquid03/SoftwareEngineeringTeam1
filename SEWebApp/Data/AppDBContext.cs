namespace SEWebApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using SEWebApp.Models;
    using System.Collections.Generic;

    public class AppDBContext : DbContext
    {
        // Database properties defined here
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }
        public DbSet<RecallNotice> RecallNotices { get; set; }
    }
}
