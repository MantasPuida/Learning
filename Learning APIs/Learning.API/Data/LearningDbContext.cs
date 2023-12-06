using Learning.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Learning.API.Data
{
    public class LearningDbContext : DbContext
    {
        public LearningDbContext(DbContextOptions<LearningDbContext> dbContextOptions): base(dbContextOptions) { }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
