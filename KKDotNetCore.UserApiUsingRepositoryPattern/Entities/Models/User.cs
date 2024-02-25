using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KKDotNetCore.UserApiUsingRepositoryPattern.Entities.Models
{
    [Table("Tbl_Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public String? UserName { get; set; }
        public String? UserEmail { get; set; }
        public String? UserPhone { get; set; }
        public String? UserAddress { get; set; }
    }
}
