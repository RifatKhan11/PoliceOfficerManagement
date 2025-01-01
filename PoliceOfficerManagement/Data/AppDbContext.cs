using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PoliceOfficerManagement.Data.Auth;
using PoliceOfficerManagement.Data.Entity;

namespace PoliceOfficerManagement.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor _httpContextAccessor) : base(options)
        {
            this._httpContextAccessor = _httpContextAccessor;
            Database.SetCommandTimeout(2000000);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        #region Settings Configs
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {

            var entities = ChangeTracker.Entries().Where(x => x.Entity is Base && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = !string.IsNullOrEmpty(_httpContextAccessor?.HttpContext?.User?.Identity?.Name)
                ? _httpContextAccessor.HttpContext.User.Identity.Name
                : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((Base)entity.Entity).createdAt = DateTime.Now;
                    ((Base)entity.Entity).createdBy = currentUsername;
                }
                else
                {
                    entity.Property("createdAt").IsModified = false;
                    entity.Property("createdBy").IsModified = false;
                    ((Base)entity.Entity).updatedAt = DateTime.Now;
                    ((Base)entity.Entity).updatedBy = currentUsername;
                }
            }

        }
        #endregion


        #region USER MANAGE
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<ActivityHistoryLog> ActivityHistoryLogs { get; set; }
        public DbSet<ActivityLogType> ActivityLogTypes { get; set; }
        #endregion

        #region MasterData
        public DbSet<RangeMetro> RangeMetros { get; set; }
        public DbSet<DivisionDistrict> DivisionDistricts { get; set; }
        public DbSet<ZoneCircle> ZoneCircles { get; set; }
        public DbSet<PoliceThana> PoliceThanas { get; set; }
        public DbSet<Rank> ranks { get; set; }
        #endregion

        #region Employee Info
        public DbSet<EmployeInfo> employeeInfos { get; set; }
        #endregion
    }
}
