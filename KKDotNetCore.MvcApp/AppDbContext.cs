using KKDotNetCore.MvcApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KKDotNetCore.MvcApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<UserDataModel> User { set; get; }
    }
}
