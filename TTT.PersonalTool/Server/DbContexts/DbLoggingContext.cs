using Microsoft.EntityFrameworkCore;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Server.DbContexts
{
    public partial class DbLoggingContext : DbContext
    {
        public DbLoggingContext()
        {
        }

        public DbLoggingContext(DbContextOptions<DbLoggingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>().HasKey(b => b.Id).HasName("Id");
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
