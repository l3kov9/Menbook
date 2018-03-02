namespace Menbook.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Beers;
    using Models.Blog;
    using Models.Cars;

    public class MenbookDbContext : IdentityDbContext<User>
    {
        public MenbookDbContext(DbContextOptions<MenbookDbContext> options)
            : base(options)
        {
        }

        public DbSet<Make> Makes { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<UserCar> UserCars { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Beer> Beers { get; set; }

        public DbSet<BeerRating> BeerRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<UserCar>()
                .HasKey(uc => new { uc.UserId, uc.ModelId });

            builder
                .Entity<User>()
                .HasMany(u => u.Cars)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            builder
                .Entity<Model>()
                .HasMany(m => m.Users)
                .WithOne(u => u.Model)
                .HasForeignKey(u => u.ModelId);

            builder
                .Entity<Make>()
                .HasMany(m => m.Models)
                .WithOne(m => m.Make)
                .HasForeignKey(m => m.MakeId);

            builder
                .Entity<Rating>()
                .HasKey(r => new { r.UserId, r.ModelId });

            builder
                .Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.CarRatings)
                .HasForeignKey(r => r.UserId);

            builder
                .Entity<Rating>()
                .HasOne(r => r.Model)
                .WithMany(u => u.UserRatings)
                .HasForeignKey(r => r.ModelId);

            builder
                .Entity<Review>()
                .HasKey(r => new { r.UserId, r.ModelId });

            builder
                .Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.CarReviews)
                .HasForeignKey(r => r.UserId);

            builder
                .Entity<Review>()
                .HasOne(r => r.Model)
                .WithMany(u => u.UserReviews)
                .HasForeignKey(r => r.ModelId);

            builder
                .Entity<BeerRating>()
                .HasKey(br => new { br.BeerId, br.UserId });

            builder
                .Entity<BeerRating>()
                .HasOne(br => br.Beer)
                .WithMany(b => b.Ratings)
                .HasForeignKey(br => br.BeerId);

            builder
                .Entity<BeerRating>()
                .HasOne(br => br.User)
                .WithMany(b => b.BeerRatings)
                .HasForeignKey(br => br.UserId);

            builder
                .Entity<Beer>()
                .HasOne(b => b.Author)
                .WithMany(u => u.BeersAdded)
                .HasForeignKey(b => b.AuthorId);

            base.OnModelCreating(builder);
        }
    }
}
