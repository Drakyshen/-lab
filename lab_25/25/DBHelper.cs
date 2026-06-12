using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
namespace _25
{
    internal class DBHelper
    {
        private static string connString =
            "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123qwe;";

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connString);
        }
    }
}
