using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdministrativeInterface.Models
{
    public class CategoryModel
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [Remote(action: "VerifyTitleUniqueness", controller:"Category", AdditionalFields = "Id")]
        public string Title { get; set; }
    }
}
