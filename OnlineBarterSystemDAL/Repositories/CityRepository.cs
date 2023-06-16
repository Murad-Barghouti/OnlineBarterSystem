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
    public class CityRepository : ICityRepository
    {
        private readonly OnlineBarterSystemContext _onlineBarterSystemContext;

        public CityRepository(OnlineBarterSystemContext onlineBarterSystemContext)
        {
            _onlineBarterSystemContext = onlineBarterSystemContext;
        }
        public async Task<City> GetCityByIdAsync(long id)
        {
            return await _onlineBarterSystemContext.Cities.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
