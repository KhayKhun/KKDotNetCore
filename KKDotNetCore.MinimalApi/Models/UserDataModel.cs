using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKDotNetCore.MinimalApi
{
    [Table("Tbl_Users")]
    public class UserDataModel
    {
        [Key]
        public int UserId { get; set; }
        public String? UserName { get; set; }
        public String? UserEmail { get; set; }
        public String? UserPhone { get; set; }
        public String? UserAddress { get; set; }
    }
}
