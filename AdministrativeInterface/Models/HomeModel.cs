using Entities;
using System.Collections.Generic;

namespace AdministrativeInterface.Models
{
    public class HomeModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
