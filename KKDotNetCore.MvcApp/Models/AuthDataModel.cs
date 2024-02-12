using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKDotNetCore.MvcApp
{
    [Table("Tbl_Auth")]
    public class AuthUserModel
    {
        [Key]
        public Guid UserId { get; set; }
        public String UserName { get; set; }
        public String FullName { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String Role { get; set; }
    }
    public class RequestPasswordChangeModel
    {
        public String Old { get; set; }
        public String New { get; set; }

    }

}
