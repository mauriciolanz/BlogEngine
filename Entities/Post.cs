using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; }
        public int CategoryId { get; set; }
        public bool VisibleInAPI
        {
            get
            {
                return this.PublicationDate <= DateTime.Now;
            }
        }
    }
}
