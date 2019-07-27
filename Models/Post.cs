using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrsTaster.Models
{
    public class Post
    {
        public int PostId { get; set; }

        [Required(ErrorMessage = "The Post should have a title!")]
        [StringLength(50, ErrorMessage = "The title can not be longer than 50 carachter!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Post should have a body!")]
        [StringLength(50, ErrorMessage = "The body can not be longer than 50 carachter!")]
        public string Body { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public int CategoryId { get; set; }
       
    }
}
