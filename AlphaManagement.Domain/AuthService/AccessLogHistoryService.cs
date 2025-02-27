using AlphaManagement.DAL;
using AlphaManagement.DAL.Entity.Auth;
using AlphaManagement.Domain.AuthService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.AuthService
{
    public class AccessLogHistoryService: IAccessLogHistoryService
    {
        private readonly AlphaDbContext _context;

        public AccessLogHistoryService(AlphaDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveUserLogHistory(UserLogHistory userLogHistory)
        {
            try
            {
                if (userLogHistory.Id != 0)
                {
                    _context.UserLogHistories.Update(userLogHistory);
                }
                else
                {
                    _context.UserLogHistories.Add(userLogHistory);
                }

                await _context.SaveChangesAsync();
                return userLogHistory.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveUnauthorizeUserLog(UnauthorizeUserLog userLogHistory)
        {
            try
            {
                if (userLogHistory.Id != 0)
                {
                    _context.unauthorizeUserLogs.Update(userLogHistory);
                }
                else
                {
                    _context.unauthorizeUserLogs.Add(userLogHistory);
                }

                await _context.SaveChangesAsync();
                return userLogHistory.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<UnauthorizeUserLog>> GetUnauthorizeUserLog()
        {
            return await _context.unauthorizeUserLogs.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<UserLogHistory>> GetAllUserLogHistory()
        {
            return await _context.UserLogHistories.Select(x => new UserLogHistory { userId = x.userId, logTime = x.logTime, ipAddress = x.ipAddress, statusName = x.status == 1 ? "Logged In" : x.status == 0 ? "Logged Out" : "Logged Off" }).ToListAsync();
        }

        public async Task<IEnumerable<UserLogHistory>> GetUserLogHistoryByUser(string userName)
        {
            return await _context.UserLogHistories.Where(x => x.createdBy == userName).ToListAsync();
        }

        public async Task<UserLogHistory> GetLastUserLogHistoryByUserId(string userName)
        {
            return await _context.UserLogHistories.Where(x => x.userId == userName).LastOrDefaultAsync();
        }

    }
}
