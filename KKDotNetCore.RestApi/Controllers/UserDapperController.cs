using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace KKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDapperController : ControllerBase
    {

        private readonly SqlConnectionStringBuilder _scsb = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "UserDb",
            UserID = "sa",
            Password = "sa@123456",
            TrustServerCertificate = true
        };

        [HttpGet]
        public IActionResult GetUsers()
        {
            using IDbConnection db = new SqlConnection(_scsb.ConnectionString); //using: Clear from memory after used. Cannot close or delete file while using.
            string query = @"SELECT [User_Id]
      ,[User_Name]
      ,[User_Age]
  FROM [UserDb].[dbo].[Tbl_User]
";
            List<UserDataModel> lst = db.Query<UserDataModel>(query).ToList();
            return Ok(lst);
        }

        [HttpPost]
        public IActionResult CreateUser(UserDataModel user)
        {
            string query = @"
INSERT INTO [dbo].[Tbl_User]
           ([User_Name]
           ,[User_Age])
     VALUES
           (@User_Name
           ,@User_Age)";

            using IDbConnection db = new SqlConnection(_scsb.ConnectionString); //using: Clear from memory after used. Cannot close or delete file while using.

            int result = db.Execute(query, user);

            string message = result > 0 ? "Saved user." : "Failed to save";

            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserDataModel user)
        {

            using IDbConnection db = new SqlConnection(_scsb.ConnectionString); //using: Clear from memory after used. Cannot close or delete file while using.

            if (string.IsNullOrEmpty(user.User_Name))
            {
                return BadRequest("Username is required");
            }
            if(user.User_Age <= 0)
            {
                return BadRequest("Age must be greater than 0");
            }

            string query = @"UPDATE [dbo].[Tbl_User]
   SET [User_Name] = @User_Name
      ,[User_Age] = @User_Age
 WHERE User_Id = @User_Id";

            int result = db.Execute(query, new UserDataModel
            {
                User_Name = user.User_Name,
                User_Age = user.User_Age,
                User_Id = id
            });

            string message = result > 0 ? "Updated user." : "Failed to update";

            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchUser(int id, UserDataModel user)
        {

            using IDbConnection db = new SqlConnection(_scsb.ConnectionString); //using: Clear from memory after used. Cannot close or delete file while using.

            string condition = string.Empty;

            if (!string.IsNullOrEmpty(user.User_Name))
            {
                condition += @" [User_Name] = @User_Name, ";
            }
            if (user.User_Age > 0)
            {
                condition += @" [User_Age] = @User_Age, ";
            }
            if (string.IsNullOrEmpty(condition))
            {
                return BadRequest("Can't update. Both username and age is required");
            }

            condition = condition.Substring(0, condition.Length - 2);

            string query = $@"UPDATE [dbo].[Tbl_User]
   SET {condition}
 WHERE User_Id = @User_Id";

            int result = db.Execute(query, new UserDataModel
            {
                User_Name = user.User_Name,
                User_Age = user.User_Age,
                User_Id = id
            });

            string message = result > 0 ? "Updated user." : "Failed to update";

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsers(int id)
        {

            string query = @"DELETE FROM [dbo].[Tbl_User]
      WHERE User_id = @User_Id";

            using IDbConnection db = new SqlConnection(_scsb.ConnectionString); //using: Clear from memory after used. Cannot close or delete file while using.

            int result = db.Execute(query, new UserDataModel
            {
                User_Id = id
            });

            string message = result > 0 ? "Deleted user." : "Failed to delete";

            return Ok(message);
        }
    }
}
