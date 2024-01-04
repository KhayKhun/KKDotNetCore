using Dapper;
using KKDotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKDotNetCore.ConsoleApp.DapperExamples
{
    internal class DapperExample
    {
        private readonly SqlConnectionStringBuilder _scsb = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "UserDb",
            UserID = "sa",
            Password = "sa@123456"
        };

        public void Run()
        {
            //Create("Created by dapper", 11);
            //Update(1, "Khay Khun", 17);
            //Delete(12);
            //Edit(2);
            //Read();
        }

        private void Read()
        {

            //            using(IDbConnection db = new SqlConnection(_scsb.ConnectionString))
            //            {
            //                string query = @"SELECT [User_Id]
            //      ,[User_Name]
            //      ,[User_Age]
            //  FROM [UserDb].[dbo].[Tbl_User]
            //";
            //                List<UserDataModel> lst = db.Query<UserDataModel>(query).ToList();
            //                foreach(UserDataModel item in lst)
            //                {
            //                    Console.WriteLine($"User_Id => {item.User_Id}");
            //                    Console.WriteLine($"User_Name => {item.User_Name}");
            //                    Console.WriteLine($"User_Age => {item.User_Age}");
            //                    Console.WriteLine("-------------------------");
            //                }
            //            }
            using IDbConnection db = new SqlConnection(_scsb.ConnectionString); //using: Clear from memory after used. Cannot close or delete file while using.
            string query = @"SELECT [User_Id]
      ,[User_Name]
      ,[User_Age]
  FROM [UserDb].[dbo].[Tbl_User]
";
                List<UserOldDataModel> lst = db.Query<UserOldDataModel>(query).ToList();
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
            using IDbConnection db = new SqlConnection(_scsb.ConnectionString); //using: Clear from memory after used. Cannot close or delete file while using.
            string query = @"SELECT [User_Id]
      ,[User_Name]
      ,[User_Age]
  FROM [UserDb].[dbo].[Tbl_User] WHERE User_Id = @User_Id
";
            UserOldDataModel? item = db.Query<UserOldDataModel>(query, new UserOldDataModel {User_Id = id}).FirstOrDefault();

             if(item is null)
            {
                Console.WriteLine("No data foun");
                return;
            }

            Console.WriteLine($"User_Id => {item.User_Id}");
            Console.WriteLine($"User_Name => {item.User_Name}");
            Console.WriteLine($"User_Age => {item.User_Age}");
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

            using IDbConnection db = new SqlConnection(_scsb.ConnectionString); //using: Clear from memory after used. Cannot close or delete file while using.

            int result = db.Execute(query, new UserOldDataModel
            {
                User_Name = name,
                User_Age = age
            });

            string message = result > 0 ? "Saved user." : "Failed to save";

            Console.Write(message);


        }

        private void Update(int id,string name, int age)
        {

            string query = @"UPDATE [dbo].[Tbl_User]
   SET [User_Name] = @User_Name
      ,[User_Age] = @User_Age
 WHERE User_Id = @User_Id";

            using IDbConnection db = new SqlConnection(_scsb.ConnectionString); //using: Clear from memory after used. Cannot close or delete file while using.

            int result = db.Execute(query, new UserOldDataModel
            {
                User_Name = name,
                User_Age = age,
                User_Id = id
            });

            string message = result > 0 ? "Updated user." : "Failed to update";

            Console.Write(message);


        }

        private void Delete(int id)
        {

            string query = @"DELETE FROM [dbo].[Tbl_User]
      WHERE User_id = @User_Id";

            using IDbConnection db = new SqlConnection(_scsb.ConnectionString); //using: Clear from memory after used. Cannot close or delete file while using.

            int result = db.Execute(query, new UserOldDataModel
            {
                User_Id = id
            });

            string message = result > 0 ? "Deleted user." : "Failed to delete";

            Console.Write(message);


        }

    }
}
