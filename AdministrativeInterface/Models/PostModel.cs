﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace AdministrativeInterface.Models
{
    public class PostModel
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [Remote(action: "VerifyTitleUniqueness", controller: "Post", AdditionalFields = "Id")]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="Pub Date")]
        public DateTime PublicationDate { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
