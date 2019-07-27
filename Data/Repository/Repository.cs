using MrsTaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrsTaster.Data.Repository
{
    public class Repository : IRepository
    {
        private AppDbContext _ctx;

        public Repository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public Category GetCategory(int id)
        {
            return _ctx.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public List<Category> GetCategories()
        {
            return _ctx.Categories.ToList();
        }

        public Post GetPost(int id)
        {
            return _ctx.Posts.FirstOrDefault(p => p.PostId == id);
        }
    
        public List<Post> GetAllPost()
        {
            return _ctx.Posts.ToList();
        }

        public void AddPost(Post post)
        {
            _ctx.Posts.Add(post);
        }

        public void UpdatePost(Post post)
        {
            _ctx.Posts.Update(post);
        }

        public void RemovePost(int id)
        {
            _ctx.Posts.Remove(GetPost(id));
        }

        public List<Post> FindPost(string value, int categoryId)
        {
            return _ctx.Posts.Where(p => p.Title.Contains(value) || p.CategoryId.Equals(categoryId)).ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            if(await _ctx.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
