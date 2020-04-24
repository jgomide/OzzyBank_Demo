
using System.Collections.Generic;
using System.Threading.Tasks;
using OzzyBank_Demo.Domain.Entities;

namespace OzzyBank_Demo.Domain.Interfaces.Service
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAll();
    }
}
