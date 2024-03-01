using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Xml.Linq;

namespace KKDotNetCore.Services
{
    public class DapperService
    {
        private readonly SqlConnectionStringBuilder _scsb;

        public DapperService(SqlConnectionStringBuilder sqlConnectionStringBuilder) {
            _scsb = sqlConnectionStringBuilder;
        }


        public List<T> Query<T>(string query, CommandType commandType = CommandType.Text, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_scsb.ConnectionString);
            List<T> lst = db.Query<T>(sql:query,param,commandType: commandType ).ToList();

            return lst;
        }

        public int Execute(string query, CommandType commandType = CommandType.Text, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_scsb.ConnectionString);
            int result = db.Execute(sql: query, param, commandType: commandType);

            return result;
        }
    }
}
