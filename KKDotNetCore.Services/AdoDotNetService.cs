using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Xml.Linq;

namespace KKDotNetCore.Services
{
    public class AdoDotNetService
    {
        private readonly SqlConnectionStringBuilder _scsb;

        public AdoDotNetService(SqlConnectionStringBuilder sqlConnectionStringBuilder) {
            _scsb = sqlConnectionStringBuilder;
        }


        //public DataTable Query(string query,CommandType commandType = CommandType.Text , params SqlParameter[] sqlParameters )
        //{
        //    SqlConnection connection = new SqlConnection(_scsb.ConnectionString);
        //    connection.Open();

        //    SqlCommand cmd = new SqlCommand(query, connection);
        //    cmd.CommandType = commandType;
        //    cmd.Parameters.AddRange(sqlParameters);

        //    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    sqlDataAdapter.Fill(dt);

        //    connection.Close();
        //    return dt;
        //}
        public List<T> Query<T>(string query,CommandType commandType = CommandType.Text , params SqlParameter[] sqlParameters )
        {
            SqlConnection connection = new SqlConnection(_scsb.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.CommandType = commandType;
            cmd.Parameters.AddRange(sqlParameters);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            var jsonStr = JsonConvert.SerializeObject(dt);

            return JsonConvert.DeserializeObject<List<T>>(jsonStr)!;
        }

        public int Execute(string query, CommandType commandType = CommandType.Text, params SqlParameter[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_scsb.ConnectionString);

            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.CommandType = commandType;
            cmd.Parameters.AddRange(sqlParameters);

            int result = cmd.ExecuteNonQuery();

            connection.Close();
            return result;
        }
    }
}
