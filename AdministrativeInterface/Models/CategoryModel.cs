using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
