using Microsoft.EntityFrameworkCore;

namespace KKDotNetCore.MinimalApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<UserDataModel> User { set; get; }
    }
}
