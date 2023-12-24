using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKDotNetCore.ConsoleApp.AdoDotNetExamples
{
    public class AdoDotNetExample
    {
        public void Run()
        {
            //Read();
            //Edit(2);
            //Edit(200);
            //Create("John",22);
            //Update(5,"Updated",22);
            //Delete(11);
        }
        private void Read()
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "UserDb",
                UserID = "sa",
                Password = "sa@123456"
            };

            SqlConnection connection = new SqlConnection(scsb.ConnectionString);

            connection.Open();

            string query = @"SELECT [User_Id]
      ,[User_Name]
      ,[User_Age]
  FROM [UserDb].[dbo].[Tbl_User]
";

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine($"User_Id => {dr["User_Id"]}");
                Console.WriteLine($"User_Name => {dr["User_Name"]}");
                Console.WriteLine($"User_Age => {dr["User_Age"]}");
                Console.WriteLine("-------------------------");
            };
        }

        private void Edit(int id)
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "UserDb",
                UserID = "sa",
                Password = "sa@123456"
            };

            SqlConnection connection = new SqlConnection(scsb.ConnectionString);

            connection.Open();

            string query = @"SELECT [User_Id]
      ,[User_Name]
      ,[User_Age]
  FROM [UserDb].[dbo].[Tbl_User] WHERE [User_Id] = @id
";
           
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id",id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            if(dt.Rows.Count == 0)
            {
                Console.Write("No row found");
                return;
            }; 

            DataRow dr = dt.Rows[0];
                Console.WriteLine($"User_Id => {dr["User_Id"]}");
                Console.WriteLine($"User_Name => {dr["User_Name"]}");
                Console.WriteLine($"User_Age => {dr["User_Age"]}");
                Console.WriteLine("-------------------------");
        }

        private void Create(string name, int age)
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "UserDb",
                UserID = "sa",
                Password = "sa@123456"
            };

            SqlConnection connection = new SqlConnection(scsb.ConnectionString);

            connection.Open();

            string query = @"
INSERT INTO [dbo].[Tbl_User]
           ([User_Name]
           ,[User_Age])
     VALUES
           (@User_Name
           ,@User_Age)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@User_Name", name);
            cmd.Parameters.AddWithValue("@User_age", age);

            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Saved user." : "Failed to save";

            Console.Write(message);

             
        }

        private void Update(int id,string name, int age)
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "UserDb",
                UserID = "sa",
                Password = "sa@123456"
            };

            SqlConnection connection = new SqlConnection(scsb.ConnectionString);

            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_User]
   SET [User_Name] = @User_Name
      ,[User_Age] = @User_Age
 WHERE User_Id = @User_Id";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@User_Name", name);
            cmd.Parameters.AddWithValue("@User_age", age);
            cmd.Parameters.AddWithValue("@User_Id", id);

            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Updated user." : "Failed to update";

            Console.Write(message);

             
        }

        private void Delete(int id)
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "UserDb",
                UserID = "sa",
                Password = "sa@123456"
            };

            SqlConnection connection = new SqlConnection(scsb.ConnectionString);

            connection.Open();

            string query = @"DELETE FROM [dbo].[Tbl_User]
      WHERE User_id = @User_Id";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@User_Id", id);

            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Deleted user." : "Failed to delete";

            Console.Write(message);

             
        }

    }
}
