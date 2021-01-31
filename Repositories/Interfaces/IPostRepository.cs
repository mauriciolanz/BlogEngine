using Entities;
using System.Collections.Generic;

namespace Repositories
{
    public interface IPostRepository
    {
        void Insert(Post post);
        void Update(Post post);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetByCategoryId(int categoryId);
        Post GetById(int id);
        Post GetByTitle(string title);
    }
}
