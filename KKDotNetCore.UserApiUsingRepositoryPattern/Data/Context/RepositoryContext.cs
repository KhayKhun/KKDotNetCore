using KKDotNetCore.UserApiUsingRepositoryPattern.Models;
using Microsoft.EntityFrameworkCore;

namespace KKDotNetCore.UserApiUsingRepositoryPattern.Entities.Context
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
