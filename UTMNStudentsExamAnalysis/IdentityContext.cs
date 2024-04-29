using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UTMNStudentsExamAnalysis.Models;

namespace UTMNStudentsExamAnalysis
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _configuration;
        public IdentityContext(DbContextOptions options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("ExamUserDatabase"));
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
