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
        private readonly IAccountRepository _accountRepository;

        public BarterRepository(OnlineBarterSystemContext onlineBarterSystemContext, IAccountRepository accountRepository)
        {
            _onlineBarterSystemContext = onlineBarterSystemContext;
            _accountRepository = accountRepository;
        }

        public async Task<Barter> ApproveBarterAsync(long id)
        {
            var barter = await GetBarterByIdAsync(id);
            if (barter == null)
            {
                return null;
            }
            var barterStates = await _onlineBarterSystemContext.BarterStates.ToListAsync();
            var successfulState = barterStates.Where(bs => bs.Name.ToLower().Equals("successful")).FirstOrDefault();
            if (successfulState != null)
            {
                barter.BarterStateId = successfulState.Id;
                var result = _onlineBarterSystemContext.Barters.Update(barter);
                await _onlineBarterSystemContext.SaveChangesAsync();
                return result.Entity;
            }
            return null;
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

        public async Task<Barter> DeleteBarterByIdAsync(long id)
        {
            var barter = await GetBarterByIdAsync(id);
            if (barter != null)
            {
                var result = _onlineBarterSystemContext.Barters.Remove(barter);
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

        public async Task<Barter> GetBarterByIdAsync(long id)
        {
            var barter = await _onlineBarterSystemContext.Barters.Include(b => b.Initiator).ThenInclude(u => u.City).Include(b => b.Joiner).Include(b => b.BarterState)
                .Include(b => b.GiveType).ThenInclude(gt => gt.Category).Include(b => b.ReceiveType).ThenInclude(rt => rt.Category).FirstOrDefaultAsync(b => b.Id == id);
            return barter;
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

        public async Task<Barter> JoinBarterAsync(long id, string userName)
        {
            var user = await _accountRepository.GetUserByUserNameAsync(userName);
            if (user == null)
            {
                return null;
            }
            var barter = await GetBarterByIdAsync(id);
            if (barter == null)
            {
                return null;
            }
            var barterStates = await _onlineBarterSystemContext.BarterStates.ToListAsync();
            var pendingApprovalState = barterStates.Where(bs => bs.Name.ToLower().Equals("pending approval")).FirstOrDefault();
            if (pendingApprovalState != null)
            {
                barter.BarterStateId = pendingApprovalState.Id;
                barter.JoinerId = user.Id;
                var result = _onlineBarterSystemContext.Barters.Update(barter);
                await _onlineBarterSystemContext.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public async Task<Barter> LeaveBarterAsync(long id)
        {
            /*
            var user = await _accountRepository.GetUserByUserNameAsync(userName);
            if (user == null)
            {
                return null;
            }
            */
            var barter = await GetBarterByIdAsync(id);
            if (barter == null)
            {
                return null;
            }
            var barterStates = await _onlineBarterSystemContext.BarterStates.ToListAsync();
            var activeState = barterStates.Where(bs => bs.Name.ToLower().Equals("active")).FirstOrDefault();
            if (activeState != null)
            {
                barter.BarterStateId = activeState.Id;
                barter.JoinerId = null;
                var result = _onlineBarterSystemContext.Barters.Update(barter);
                await _onlineBarterSystemContext.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public async Task<Barter> RejectBarterAsync(long id)
        {
            var barter = await GetBarterByIdAsync(id);
            if (barter == null)
            {
                return null;
            }
            var barterStates = await _onlineBarterSystemContext.BarterStates.ToListAsync();
            var activeState = barterStates.Where(bs => bs.Name.ToLower().Equals("active")).FirstOrDefault();
            if (activeState != null)
            {
                barter.BarterStateId = activeState.Id;
                barter.JoinerId = null;
                var result = _onlineBarterSystemContext.Barters.Update(barter);
                await _onlineBarterSystemContext.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public async Task<Barter> UpdateBarterAsync(long id, double? giveValue, double? receiveValue, string? description)
        {
            var barter = await GetBarterByIdAsync(id);
            if (barter != null)
            {
                if (giveValue != null)
                {
                    barter.GiveValue = giveValue;
                }
                if (receiveValue != null)
                {
                    barter.ReceiveValue = receiveValue;
                }
                if (description != null)
                {
                    barter.Description = description;
                }
                var result = _onlineBarterSystemContext.Barters.Update(barter);
                await _onlineBarterSystemContext.SaveChangesAsync();

                return result.Entity;
            }
            return null;
        }
    }
}