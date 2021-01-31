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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IPostRepository postRepository;

        public CategoriesController(ICategoryRepository categoryRepository, IPostRepository postRepository)
        {
            this.categoryRepository = categoryRepository;
            this.postRepository = postRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var categoryList = categoryRepository.GetAll();

            if (categoryList != null && categoryList.Count() > 0)
            {
                return Ok(categoryList);
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = categoryRepository.GetById(id);

            if (category != null)
            {
                return Ok(category);
            }

            return NotFound();
        }

        [HttpGet("{id}/posts")]
        public IActionResult GetPostsByCategory(int id)
        {
            var postList = postRepository.GetByCategoryId(id)
                .Where(post => post.VisibleInAPI)
                .OrderByDescending(post => post.PublicationDate);

            if (postList != null && postList.Count() > 0)
            {
                return Ok(postList);
            }

            return NoContent();
        }
    }
}
