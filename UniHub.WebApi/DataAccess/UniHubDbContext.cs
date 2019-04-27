using Microsoft.EntityFrameworkCore;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess
{
    public class UniHubDbContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<RoleType> RoleTypes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<PostAction> PostActions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerVote> AnswerVotes { get; set; }
        public DbSet<PostLocationType> PostLocationTypes { get; set; }
        public DbSet<PostValueType> PostValueTypes { get; set; }
        public DbSet<FileType> FileTypes { get; set; }
        public DbSet<PostActionType> PostActionTypes { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
 
        public UniHubDbContext(DbContextOptions<UniHubDbContext> options)
         : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(p => p.CurrencyCount).HasDefaultValue(0);
            });
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasIndex(c => c.Title).IsUnique();
            });
        }
    }
}