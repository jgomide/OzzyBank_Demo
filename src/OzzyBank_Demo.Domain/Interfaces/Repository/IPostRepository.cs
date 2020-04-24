using System.Collections.Generic;
using System.Threading.Tasks;
using OzzyBank_Demo.Domain.Entities;

namespace OzzyBank_Demo.Domain.Interfaces.Repository
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAll();
        

    }
}
