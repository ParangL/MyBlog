using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private IRepository _repo;
       
        public PanelController(IRepository repo)
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

        [HttpGet]
        public IActionResult Edit()
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostViewModel viewpost)
        {
            Post post = new Post();
            post.Title = viewpost.Title;
            post.Body = viewpost.Body;            
            post.CategoryId = int.Parse(viewpost.CurrentCategory);
            post.Created = viewpost.Created;
            _repo.AddPost(post);

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");

            else
                return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");
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
