using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKDotNetCore.ConsoleApp.Models
{
    [Table("Tbl_User")]
    public class UserDataModel
    {
        [Key]
        [Column("User_Id")] // in case if names not match with names from db
        public int User_Id { get; set; } // default 0 for 'int'
        public string? User_Name { get; set; } 
        public int User_Age { get; set; } 
    }
}
