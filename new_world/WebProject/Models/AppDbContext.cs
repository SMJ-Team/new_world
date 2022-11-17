using Microsoft.EntityFrameworkCore;

namespace WebProject.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {

        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<Land> Lands { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Area newArea = new Area { ID = 1, Name = "The first area" };
            modelBuilder.Entity<Area>().HasData(newArea);

            int count = 0;
            for (int i = 1; i <= 14; i++)
            {
                for (int j = 1; j <= 23; j++)
                {
                    count++;
                    modelBuilder.Entity<Land>().HasData(new Land { ID = count, X = j, Y = i, AreaId = 1 }) ; //adding lands to DB with assigned coordinates
                }
            }
            
        }
    }
}
