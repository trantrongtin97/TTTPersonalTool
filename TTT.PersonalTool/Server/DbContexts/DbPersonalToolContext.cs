using Microsoft.EntityFrameworkCore;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Server.DbContexts
{
    public partial class DbPersonalToolContext : DbContext
    {
        public DbPersonalToolContext()
        {
        }
        public DbPersonalToolContext(DbContextOptions<DbPersonalToolContext> options)
            : base(options)
        {
        }

        #region Define Entity
        public virtual DbSet<User> Users { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(b => b.Id).HasName("Id");
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
