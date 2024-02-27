using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKDotNetCore.RealtimeChartMvcApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TeamDataModel> Team { set; get; }
    }

    [Table("Tbl_team")]
    public class TeamDataModel{
        [Key]
        public int TeamId { get; set; }
        public int Score { get; set; }
        public string TeamName {  get; set; }

        }
}

