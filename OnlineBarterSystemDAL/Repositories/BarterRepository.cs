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
    public class BarterRepository : IBarterRepository
    {
        private readonly OnlineBarterSystemContext _onlineBarterSystemContext;

        public BarterRepository(OnlineBarterSystemContext onlineBarterSystemContext)
        {
            _onlineBarterSystemContext = onlineBarterSystemContext;
        }

        public async Task<Barter> CreateBarter(long initiatorId, long giveTypeId, long receiveTypeId, double? giveValue,
            double? receiveValue, string? description)
        {
            var barterStates = await _onlineBarterSystemContext.BarterStates.ToListAsync();
            var activeState = barterStates.Where(bs => bs.Name.ToLower().Equals("active")).FirstOrDefault();
            if (activeState != null)
            {
                var barter = new Barter()
                {
                    InitiatorId = initiatorId,
                    GiveTypeId = giveTypeId,
                    ReceiveTypeId = receiveTypeId,
                    BarterStateId = activeState.Id,
                    GiveValue = giveValue,
                    ReceiveValue = receiveValue,
                    Description = description
                };
                var result = await _onlineBarterSystemContext.Barters.AddAsync(barter);
                await _onlineBarterSystemContext.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public async Task<List<Barter>> GetAllBartersAsync()
        {
            return await _onlineBarterSystemContext.Barters.Include(b => b.Initiator).ThenInclude(u => u.City).Include(b => b.Joiner).Include(b => b.BarterState)
                .Include(b => b.GiveType).ThenInclude(gt => gt.Category).Include(b => b.ReceiveType).ThenInclude(rt => rt.Category)
                .ToListAsync();
        }

        public async Task<List<Barter>> GetUserBartersAsync(long id)
        {
            return await _onlineBarterSystemContext.Barters.Where(b => b.InitiatorId == id).Include(b => b.Initiator)
                .ThenInclude(u => u.City).Include(b => b.Joiner).Include(b => b.BarterState).Include(b => b.GiveType)
                .ThenInclude(gt => gt.Category).Include(b => b.ReceiveType).ThenInclude(rt => rt.Category)
                .ToListAsync();
        }

        public async Task<Tuple<bool, string>> IsBarterValidAsync(long giveTypeId, long receiveTypeId, double? giveValue, double? receiveValue)
        {
            bool valid = true;
            long notSetId = (await _onlineBarterSystemContext.SubCategories.FirstOrDefaultAsync(sb => sb.Name.ToLower().Equals("not set"))).Id;
            long donationsId = (await _onlineBarterSystemContext.SubCategories.FirstOrDefaultAsync(sb => sb.Name.ToLower().Equals("donations"))).Id;
            if (giveTypeId == receiveTypeId && giveTypeId == notSetId)
            {
                return new Tuple<bool, string>(false, "Both give and receive types cant be not set");
            }
            else if (giveTypeId == donationsId)
            {
                return new Tuple<bool, string>(false, "Cant request a donation");
            }
            else if (giveTypeId == notSetId && receiveTypeId == donationsId)
            {
                return new Tuple<bool, string>(false, "What is being donated must be specified");
            }
            else if (giveTypeId != notSetId && giveTypeId != donationsId && receiveTypeId != notSetId && receiveTypeId != donationsId 
                && (giveValue == null || receiveValue == null))
            {
                return new Tuple<bool, string>(false, "One or more barter value not specified");
            }
            else
            {
                return new Tuple<bool, string>(true, "");
            }

        }
    }
}