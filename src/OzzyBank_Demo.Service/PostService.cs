using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OzzyBank_Demo.Domain.Entities;
using OzzyBank_Demo.Domain.Interfaces.Repository;
using OzzyBank_Demo.Domain.Interfaces.Service;

namespace OzzyBank_Demo.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {

            var result = await _postRepository.GetAll();
            
            return result.ToList();

        }
    }
}
