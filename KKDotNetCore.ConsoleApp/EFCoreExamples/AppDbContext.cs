using KKDotNetCore.ConsoleApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKDotNetCore.ConsoleApp.EFCoreExamples
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder _scsb = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "UserDb",
                UserID = "sa",
                Password = "sa@123456",
                TrustServerCertificate = true,
            };
            optionsBuilder.UseSqlServer(_scsb.ConnectionString);


        }
        public DbSet<UserOldDataModel> Users { set; get; }
    }
}
