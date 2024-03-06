using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKDotNetCore.Models
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
