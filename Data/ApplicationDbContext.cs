using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuration for the many-to-many Relationship
            //1.
            modelBuilder.Entity<Actor_Movie>()
                .HasKey(am => new { am.ActorId, am.MovieId });

            //2. break it up to 2 one-many Relationships
            //one-many Relationship
            modelBuilder.Entity<Actor_Movie>()
                .HasOne(m => m.Movie)
                .WithMany(am => am.Actors_Movies)
                .HasForeignKey(m => m.MovieId);
            //one-many Relationship
            modelBuilder.Entity<Actor_Movie>()
               .HasOne(m => m.Actor)
               .WithMany(am => am.Actors_Movies)
               .HasForeignKey(m => m.ActorId);

            base.OnModelCreating(modelBuilder);

            //To override the table names in DB
            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "security");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "security");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");

            modelBuilder.Entity<ApplicationUser>()
                .Property(m => m.ProfilePicture)
                .IsRequired(false);

            modelBuilder.Entity<ApplicationUser>()
                .Property(m => m.FirstName)
                .IsRequired(false);

            modelBuilder.Entity<ApplicationUser>()
                .Property(m => m.LastName)
                .IsRequired(false);

        }


        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }


        //Order Related Tables
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}