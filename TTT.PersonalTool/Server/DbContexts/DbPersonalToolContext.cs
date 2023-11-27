using Microsoft.EntityFrameworkCore;
using TTT.PersonalTool.Server.Services.IServices;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Server.DbContexts
{
    public partial class DbPersonalToolContext : DbContext
    {
        public DbPersonalToolContext()
        {
        }
        public DbPersonalToolContext(DbContextOptions<DbPersonalToolContext> options, IControlDataProvider userData)
            : base(options)
        {
            _tenantCode = userData.TenantCode;
        }
        private readonly string _tenantCode;
        #region Define Entity
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Tenant> Tenant { get; set; }
        public virtual DbSet<Item> Items { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>().HasKey(b => b.Id).HasName("Id");
            modelBuilder.Entity<User>().HasKey(b => b.Id).HasName("Id");
            modelBuilder.Entity<Item>().HasQueryFilter(x => x.TenantCode == _tenantCode).HasKey(b => b.Id).HasName("Id");
            OnModelCreatingPartial(modelBuilder);
        }
        //.HasQueryFilter(x => x.TenantCode == _tenantCode)
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
