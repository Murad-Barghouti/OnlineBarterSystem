using OnlineBarterSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBarterSystemDAL.Interfaces
{
    public interface IBarterRepository
    {
        Task<Tuple<bool, string>> IsBarterValidAsync(long giveTypeId, long receiveTypeId, double? giveValue, double? receiveValue);
        Task<Barter> CreateBarter(long initiatorId, long giveTypeId, long receiveTypeId, double? giveValue,
            double? receiveValue, string? description);
        Task<List<Barter>> GetAllBartersAsync();
        Task<List<Barter>> GetUserBartersAsync(long id);
    }
}
