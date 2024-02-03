using Microsoft.EntityFrameworkCore;
using Walks.API.Models.Domain;

namespace Walks.API.Data
{
    public class WalksDbContext : DbContext
    {
        public WalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
           
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed data for Difficulties
            //Easy, Medium and Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("d14055f3-9faf-4957-af7c-62050c75c3f1"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("ffb6f7dc-9f93-46d6-b6dc-5c4e19be1d85"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("3ec94a58-1ad5-4df0-ba59-5bc3470720a0"),
                    Name = "Hard"
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //Seed data for regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("3762c9d5-c95c-4ad0-97fc-478ccd7f0ae4"),
                    Name = "Siddaganga Hills",
                    Code = "TK",
                    RegionImageUrl = "https://www.google.com/imgres?imgurl=https%3A%2F%2Fi0.wp.com%2Fstepstogether.in%2Fwp-content%2Fuploads%2F2017%2F04%2FTpic-12.jpg%3Ffit%3D2520%252C1301%26ssl%3D1&tbnid=HBhd1gFODnJP8M&vet=12ahUKEwjcibm3p8OCAxULUGwGHbfZABgQMygcegUIARCRAQ..i&imgrefurl=https%3A%2F%2Fstepstogether.in%2F2017%2F12%2F17%2Fshivagange%2F&docid=PQ0xc9_VwSETiM&w=2520&h=1301&q=siddagange%20hills&ved=2ahUKEwjcibm3p8OCAxULUGwGHbfZABgQMygcegUIARCRAQ"
                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
            
        }
    }
}
