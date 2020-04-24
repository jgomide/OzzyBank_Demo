using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzzyBank_Demo.Api.Contract.V1;
using OzzyBank_Demo.Domain.Entities;
using OzzyBank_Demo.Domain.Interfaces.Service;

namespace OzzyBank_Demo.Api.Controllers.V1
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        //"api/v1/posts"
        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _postService.GetAll();
            
            return Ok(result);
        }

        /*
        private List<Post> _posts;

        public PostsController()
        {
            _posts = new List<Post>();

            for (var i = 0; i < 5; i++)
            {
                _posts.Add(new Post { Id = Guid.NewGuid().ToString() });
            }
        }

        //"api/v1/posts"
        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
          return Ok(_posts);
        }*/
    }
}


