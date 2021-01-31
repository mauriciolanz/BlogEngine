using AdministrativeInterface.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdministrativeInterface.Extensions
{
    public static class Translator
    {
        public static CategoryModel ToModel(this Category categoryEntity)
        {
            var model = new CategoryModel();
            model.Id = categoryEntity.Id;
            model.Title = categoryEntity.Title;

            return model;
        }

        public static Category ToEntity(this CategoryModel categoryModel)
        {
            var entity = new Category();
            entity.Id = categoryModel.Id.GetValueOrDefault();
            entity.Title = categoryModel.Title;

            return entity;
        }

        public static PostModel ToModel(this Post postEntity)
        {
            var model = new PostModel();
            model.Id = postEntity.Id;
            model.Title = postEntity.Title;
            model.Content = postEntity.Content;
            model.CategoryId = postEntity.CategoryId;
            model.PublicationDate = postEntity.PublicationDate;

            return model;
        }

        public static Post ToEntity(this PostModel postModel)
        {
            var entity = new Post();
            entity.Id = postModel.Id.GetValueOrDefault();
            entity.Title = postModel.Title;
            entity.Content = postModel.Content;
            entity.CategoryId = postModel.CategoryId;
            entity.PublicationDate = postModel.PublicationDate;

            return entity;
        }
    }
}
