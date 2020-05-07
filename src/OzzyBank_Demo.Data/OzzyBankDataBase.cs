using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;
using OzzyBank_Demo.Domain.Interfaces.Repository;

namespace OzzyBank_Demo.Repository
{
    public class OzzyBankDatabase : IOzzyBankDatabase
    {
        private readonly IDatabaseConfiguration _config;

        public OzzyBankDatabase(IDatabaseConfiguration config)
        {
            _config = config;
        }

        public async Task<DbConnection> CreateAndOpenConnection(CancellationToken stoppingToken = default)
        {
            Console.WriteLine("Test3:A " + _config.ConnectionString);

            var connection = new NpgsqlConnection(_config.ConnectionString);
            
            await connection.OpenAsync(stoppingToken);

            Console.WriteLine("Test3:B");

            return connection;
        }
    }
}