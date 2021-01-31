using AdministrativeInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace AdministrativeInterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IPostRepository postRepository;

        public HomeController(ICategoryRepository categoryRepository, IPostRepository postRepository)
        {
            this.categoryRepository = categoryRepository;
            this.postRepository = postRepository;
        }

        public IActionResult Index()
        {
            var model = new HomeModel();
            model.Posts = postRepository.GetAll();
            model.Categories = categoryRepository.GetAll();

            return View(model);
        }
    }
}
