using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace TFG.Models
{
    public partial class TFGContext : DbContext
    {

        public TFGContext()
        {
        }

        public TFGContext(DbContextOptions<TFGContext> options)
            : base(options)
        {
        }

        public virtual DbSet<InstagramLog> InstagramLogs { get; set; }
        public virtual DbSet<InstagramMedia> InstagramMedias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<InstagramLog>(entity =>
            {
                entity.ToTable("InstagramLog");
            });

            modelBuilder.Entity<InstagramMedia>(entity =>
            {
                entity.ToTable("InstagramMedia");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
