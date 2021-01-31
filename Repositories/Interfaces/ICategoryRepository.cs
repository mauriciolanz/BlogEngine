using Entities;
using System.Collections.Generic;

namespace Repositories
{
    public interface ICategoryRepository
    {
        void Insert(Category category);
        void Update(Category category);
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        Category GetByTitle(string title);
    }
}
