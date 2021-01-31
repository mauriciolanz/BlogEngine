using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdministrativeInterface.Extensions;
using AdministrativeInterface.Models;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace AdministrativeInterface.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository postRepository;
        private readonly ICategoryRepository categoryRepository;

        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            this.postRepository = postRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = categoryRepository.GetAll().ToList();
            return View("AddEdit");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id > 0)
            {
                Post post = postRepository.GetById(id);

                if (post != null)
                {
                    ViewBag.Categories = categoryRepository.GetAll().ToList();
                    return View("AddEdit", post.ToModel());
                }
            }

            return View("AddEdit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(PostModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Id > 0)
                        postRepository.Update(model.ToEntity());
                    else
                        postRepository.Insert(model.ToEntity());
                }
                catch(Exception ex)
                {
                    return AddEditViewWithError(model, GetErrorMessages(ex));
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return AddEditViewWithError(model, GetErrorMessages());
            }
        }

        private IActionResult AddEditViewWithError(PostModel model, List<string> errorMessages)
        {
            ViewBag.ErrorMessages = errorMessages;
            ViewBag.Categories = categoryRepository.GetAll().ToList();
            return View("AddEdit", model);
        }

        [HttpGet]
        public IActionResult VerifyTitleUniqueness(string title, int id)
        {
            var post = postRepository.GetByTitle(title);

            if (post != null && post.Id != id)
                return Json(string.Format(Constants.PostDuplicatedTitleErrorMessage, title));
            else
                return Json(true);
        }

        private List<string> GetErrorMessages()
        {
            var errorMessages = new List<string>();
            foreach (var value in ModelState.Values.Where(m => m.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid))
            {
                foreach (var errorMessage in value.Errors)
                    errorMessages.Add(errorMessage.ErrorMessage);
            }

            return errorMessages;
        }

        private List<string> GetErrorMessages(Exception ex)
        {
            var errorMessages = new List<string>();
            errorMessages.Add(string.Format(Constants.PostSaveErrorMessage, ex.Message));

            return errorMessages;
        }
    }
}
