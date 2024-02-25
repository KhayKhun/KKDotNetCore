using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KKDotNetCore.UserApiUsingRepositoryPattern.Models
{
    [Table("Tbl_Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhone { get; set; }
        public string? UserAddress { get; set; }
    }
}
