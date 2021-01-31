using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdministrativeInterface.Models
{
    public class HomeModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
