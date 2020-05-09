using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace OzzyBank_Demo.Domain.Interfaces.Repository
{
    public interface IDatabaseConfiguration
    {
        string ConnectionString { get; }

        Task<IDatabaseConfiguration> Create(IConfiguration configuration);
        //public DatabaseConfiguration Create(IConfiguration configuration);
    }
}
