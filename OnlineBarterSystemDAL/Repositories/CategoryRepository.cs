using Microsoft.EntityFrameworkCore;
using OnlineBarterSystemDAL.Interfaces;
using OnlineBarterSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBarterSystemDAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly OnlineBarterSystemContext _onlineBarterSystemContext;

        public CategoryRepository(OnlineBarterSystemContext onlineBarterSystemContext)
        {
            _onlineBarterSystemContext = onlineBarterSystemContext;
        }
        public async Task<List<Category>> GetCategories()
        {
            var categories = await _onlineBarterSystemContext.Categories.Include(c => c.SubCategories).ToListAsync();
            return categories;
        }
    }
}
