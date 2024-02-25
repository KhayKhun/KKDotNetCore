using KKDotNetCore.UserApiUsingRepositoryPattern.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace KKDotNetCore.UserApiUsingRepositoryPattern
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<User>? User { get; set; }
    }
}
