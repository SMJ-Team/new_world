using Microsoft.EntityFrameworkCore;

namespace WebProject.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {

        }

        public DbSet<Land> lands { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            int count = 0;
            for (int i = 1; i <= 14; i++)
            {
                for (int j = 1; j <= 23; j++)
                {
                    count++;
                    modelBuilder.Entity<Land>().HasData(new Land { Id = count, X = j, Y = i }) ; //adding lands to DB with assigned coordinates
                }
            }
            
        }
    }
}
