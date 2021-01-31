using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace BlogAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository postRepository;

        public PostsController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var postList = postRepository.GetAll()
                .Where(post => post.VisibleInAPI)
                .OrderByDescending(post => post.PublicationDate);

            if(postList != null && postList.Count() > 0)
            {
                return Ok(postList);
            }
            
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var post = postRepository.GetById(id);

            if (post != null && post.VisibleInAPI)
            {
                return Ok(post);
            }

            return NotFound();
        }
    }
}
