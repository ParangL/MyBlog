using MrsTaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrsTaster.Data.Repository
{
    public interface IRepository
    {
        Category GetCategory(int id);
        List<Category> GetCategories();
        Post GetPost(int id);
        List<Post> GetAllPost();
        void AddPost(Post post);
        void UpdatePost(Post post);
        void RemovePost(int id);
        List<Post> FindPost(string value, int categoryId);

        Task<bool> SaveChangesAsync();
        
    }
}
