using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdministrativeInterface.Models;
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
