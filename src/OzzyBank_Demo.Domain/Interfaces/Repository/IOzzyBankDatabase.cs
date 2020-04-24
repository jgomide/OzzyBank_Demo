using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace OzzyBank_Demo.Domain.Interfaces.Repository
{
    public interface IOzzyBankDatabase
    {
        Task<DbConnection> CreateAndOpenConnection(CancellationToken stoppingToken = default);
    }
}