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
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly OnlineBarterSystemContext _onlineBarterSystemContext;

        public SubCategoryRepository(OnlineBarterSystemContext onlineBarterSystemContext)
        {
            _onlineBarterSystemContext = onlineBarterSystemContext;
        }
        public async Task<SubCategory> GetSubCategoryByIdAsync(long id)
        {
            var subCategory = await _onlineBarterSystemContext.SubCategories.FirstOrDefaultAsync(sc => sc.Id == id);
            return subCategory;
        }
    }
}
