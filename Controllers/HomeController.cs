using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrsTaster.Data;
using MrsTaster.Data.Repository;
using MrsTaster.Models;
using MrsTaster.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrsTaster.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repo;
       
        public HomeController(IRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var posts = _repo.GetAllPost();

            return View(posts);
        }

        public IActionResult Post(int id)
        {
            PostViewModel model = new PostViewModel();
            model.GetPost = _repo.GetPost(id);
            model.Title = model.GetPost.Title;
            model.Body = model.GetPost.Body;

            model.CurrentCategory = _repo.GetCategory(model.GetPost.CategoryId).CategoryName;

            return View(model);
        }

        public IActionResult Search()
        {
            PostViewModel model = new PostViewModel();

            List<Category> categories = _repo.GetCategories();

            model.CategoryList = (from c in categories
                                  select new SelectListItem
                                  {
                                      Text = c.CategoryName,
                                      Value = c.CategoryId.ToString()
                                  }).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Search(PostViewModel values)
        {
            PostViewModel model = new PostViewModel();


            model.SearchPosts = _repo.FindPost(values.SearchValue, int.Parse(values.CurrentCategory));


            return View(model);

        }       
    }
}
