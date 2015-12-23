using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace LuckyPlay.Controllers
{   
    public class LogContext : DbContext
    {
        public DbSet<LogModel> tblLogRecord { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            modelBuilder.Configurations.Add(new PartsTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class PartsTypeConfiguration : EntityTypeConfiguration<LogModel>
    {
        public PartsTypeConfiguration()
        {
            ToTable("tblLogRecord");
        }
    }
}