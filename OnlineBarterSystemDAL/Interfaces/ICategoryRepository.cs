using OnlineBarterSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBarterSystemDAL.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
    }
}
