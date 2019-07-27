using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrsTaster.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrsTaster.ViewModels
{
    public class PostViewModel
    {
        public int PostId { get; set; }
        [Required(ErrorMessage = "Write a title!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Write a text!")]
        public string Body { get; set; }
        public Post GetPost { get; set; }
        public List<Post> GetAllPost { get; set; }
        

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CurrentCategory { get; set; }
        public List<SelectListItem> CategoryList { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public List<Post> SearchPosts { get; set; }
        public string SearchValue { get; set; }

        public PostViewModel()
        {
            CategoryList = new List<SelectListItem>();
            SearchPosts = new List<Post>();
        }

    }
}
