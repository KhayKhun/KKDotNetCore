using Azure;
using Dapper;
using KKDotNetCore.ConsoleApp.Models;
using KKDotNetCore.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKDotNetCore.ConsoleApp.DapperExamples
{
    internal class DapperExample2
    {
        private readonly DapperService _service = new DapperService(new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "UserDb",
            UserID = "sa",
            Password = "sa@123456",
            TrustServerCertificate = true,
        });

        public void Run()
        {
            //Create("Created by dapper", 11);
            Update(28, "Khay Khun Mong", 28);
            //Delete(27);
            //Edit(28);
            Read();
        }

        private void Read()
        {
            string query = @"SELECT [User_Id]
      ,[User_Name]
      ,[User_Age]
  FROM [UserDb].[dbo].[Tbl_User]
";
            List<UserOldDataModel> lst = _service.Query<UserOldDataModel>(query);

                foreach(UserOldDataModel item in lst)
                {
                    Console.WriteLine($"User_Id => {item.User_Id}");
                    Console.WriteLine($"User_Name => {item.User_Name}");
                    Console.WriteLine($"User_Age => {item.User_Age}");
                    Console.WriteLine("-------------------------");
                }
        }

        private void Edit(int id)
        {
            string query = @"SELECT [User_Id]
      ,[User_Name]
      ,[User_Age]
  FROM [UserDb].[dbo].[Tbl_User] WHERE User_Id = @User_Id
";          
            UserOldDataModel user = _service.Query<UserOldDataModel>(query, param: new UserOldDataModel { User_Id = id }).FirstOrDefault();

            if (user is null)
            {
                Console.WriteLine("No data foun");
                return;
            }

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

            int result = _service.Execute(query, param: new UserOldDataModel { User_Name = name, User_Age = age });


            string message = result > 0 ? "Saved user." : "Failed to save";

            Console.Write(message);
        }

        private void Update(int id,string name, int age)
        {
            string query = @"UPDATE [dbo].[Tbl_User]
   SET [User_Name] = @User_Name
      ,[User_Age] = @User_Age
 WHERE User_Id = @User_Id";

            int result = _service.Execute(query, param: new UserOldDataModel { User_Id=id, User_Name = name, User_Age = age });


            string message = result > 0 ? "Updated user." : "Failed to update";

            Console.Write(message);
        }

        private void Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_User]
      WHERE User_id = @User_Id";

            int result = _service.Execute(query, param: new UserOldDataModel { User_Id = id});

            string message = result > 0 ? "Deleted user." : "Failed to delete";

            Console.Write(message);
        }

    }
}
