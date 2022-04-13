using Fnyo.Learn.Jenkins.Entity;
using Microsoft.EntityFrameworkCore;

namespace Fnyo.Learn.Jenkins.Context
{
    public class TmsDbContext:DbContext
    {
        public TmsDbContext(DbContextOptions<TmsDbContext> options):base(options)
        { } 

        public DbSet<Student> Students { get; set; }    
        public DbSet<Book> Books { get;set; }
        public DbSet<User> Users { get; set; }
        public DbSet<StudentScore> StudentScores { get; set; }
    }
}
