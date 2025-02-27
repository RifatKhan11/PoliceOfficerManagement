using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.SMSService.interfaces
{
    public interface ISMSService
    {
        Task<string> SendSMSAsync(string mobile, string message);
    }
}
