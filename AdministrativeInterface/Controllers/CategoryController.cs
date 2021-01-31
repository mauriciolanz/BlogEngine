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
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View("AddEdit");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if(id > 0)
            {
                Category category = categoryRepository.GetById(id);

                if(category != null)
                {
                    return View("AddEdit", category.ToModel());
                }
            }

            return View("AddEdit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Id > 0)
                        categoryRepository.Update(model.ToEntity());
                    else
                        categoryRepository.Insert(model.ToEntity());
                }
                catch(Exception ex)
                {
                    ViewBag.ErrorMessages = GetErrorMessages(ex);
                    return View("AddEdit", model);
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessages = GetErrorMessages();
                return View("AddEdit", model);
            }
        }

        [HttpGet]
        public IActionResult VerifyTitleUniqueness(string title, int id)
        {
            var category = categoryRepository.GetByTitle(title);

            if(category != null && category.Id != id)
                return Json(string.Format(Constants.CategoryDuplicatedTitleErrorMessage, title));
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

        private static List<string> GetErrorMessages(Exception ex)
        {
            var errorMessages = new List<string>();
            errorMessages.Add(string.Format(Constants.CategorySaveErrorMessage, ex.Message));

            return errorMessages;
        }
    }
}
