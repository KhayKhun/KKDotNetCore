using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using KKDotNetCore.ConsoleApp.Models;
using KKDotNetCore.Services;
using Microsoft.Data.SqlClient;

namespace KKDotNetCore.ConsoleApp.AdoDotNetExamples
{
    public class AdoDotNetExample2
    {
        private readonly AdoDotNetService _service = new AdoDotNetService(new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "UserDb",
            UserID = "sa",
            Password = "sa@123456",
            TrustServerCertificate = true,
        });

        public void Run()
        {
            Read();
            //Edit(2);
            //Edit(200);
            //Create("John", 22);
            //Update(5,"Updated",22);
            //Delete(13);
        }

        private void Read()
        {
            string query = @"SELECT [User_Id]
      ,[User_Name]
      ,[User_Age]
  FROM [UserDb].[dbo].[Tbl_User]";

            var lst = _service.Query<UserOldDataModel>(query);
            Console.WriteLine("---------------------");
            foreach (UserOldDataModel item in lst)
            {
                Console.WriteLine($"User_Id => {item.User_Id}");
                Console.WriteLine($"User_Name => {item.User_Name}");
                Console.WriteLine($"User_Age => {item.User_Age}");
                Console.WriteLine("-------------------------");
            }
            Console.WriteLine("---------------------");
        }

        private void Edit(int id)
        {
            string query = @"SELECT [User_Id]
      ,[User_Name]
      ,[User_Age]
  FROM [UserDb].[dbo].[Tbl_User] WHERE [User_Id] = @id
"; List<SqlParameter> lstParam = new List<SqlParameter>() {
                new("@id",id)
            };

            UserOldDataModel user = _service.Query<UserOldDataModel>(query, sqlParameters: lstParam.ToArray()).FirstOrDefault();


            Console.WriteLine($"User_Id => {user.User_Id}");
            Console.WriteLine($"User_Name => {user.User_Name}");
            Console.WriteLine($"User_Age => {user.User_Age}");
            Console.WriteLine("-------------------------");
        }

        private void Create(string name, int age)
        {
            string query = @"
INSERT INTO [dbo].[Tbl_User]
           ([User_Name]
           ,[User_Age])
     VALUES
           (@User_Name
           ,@User_Age)";

            List<SqlParameter> lstParam = new List<SqlParameter>() {
                new("@User_Name",name),
                new("@User_Age",age)
            };

            var result = _service.Execute(query, sqlParameters: lstParam.ToArray());

            string message = result > 0 ? "Saved user." : "Failed to save";

            Console.Write(message);


        }

        private void Update(int id, string name, int age)
        {
            string query = @"UPDATE [dbo].[Tbl_User]
   SET [User_Name] = @User_Name
      ,[User_Age] = @User_Age
 WHERE User_Id = @User_Id";


            List<SqlParameter> lstParam = new List<SqlParameter>() {
                new("@User_Name",name),
                new("@User_Id",id),
                new("@User_Age",age)
            };

            var result = _service.Execute(query, sqlParameters: lstParam.ToArray());
            string message = result > 0 ? "Updated user." : "Failed to update";

            Console.Write(message);


        }

        private void Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_User]
      WHERE User_id = @User_Id";

            List<SqlParameter> lstParam = new List<SqlParameter>() {
                new("@User_Id",id),
            };

            var result = _service.Execute(query, sqlParameters: lstParam.ToArray());
            string message = result > 0 ? "Deleted user." : "Failed to delete";

            Console.Write(message);
        }
    }
}
