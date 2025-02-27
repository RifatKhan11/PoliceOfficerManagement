using AlphaManagement.DAL.Entity.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.AuthService.Interfaces
{
    public interface IAccessLogHistoryService
    {
        Task<int> SaveUserLogHistory(UserLogHistory userLogHistory);

        Task<IEnumerable<UserLogHistory>> GetAllUserLogHistory();

        Task<IEnumerable<UserLogHistory>> GetUserLogHistoryByUser(string userName);

        Task<int> SaveUnauthorizeUserLog(UnauthorizeUserLog userLogHistory);

        Task<IEnumerable<UnauthorizeUserLog>> GetUnauthorizeUserLog();

        Task<UserLogHistory> GetLastUserLogHistoryByUserId(string userName);
    }
}
 