using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace WebApplication1.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        public readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }


        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
