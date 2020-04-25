using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;
using OzzyBank_Demo.Domain.Interfaces.Repository;

namespace OzzyBank_Demo.Repository
{
    public class OzzyBankDatabase : IOzzyBankDatabase
    {
        private readonly DatabaseConfiguration _config;

        public OzzyBankDatabase(DatabaseConfiguration config)
        {
            _config = config;
        }

        public async Task<DbConnection> CreateAndOpenConnection(CancellationToken stoppingToken = default)
        {
            var connection = new NpgsqlConnection(_config.ConnectionString); 

            await connection.OpenAsync(stoppingToken); 
            
            return connection;
        }
    }
}
