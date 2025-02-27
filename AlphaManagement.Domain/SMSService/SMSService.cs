using AlphaManagement.Domain.SMSService.interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.SMSService
{
    public class SMSService: ISMSService
    {
        public SMSService()
        {

        }
        public async Task<string> SendSMSAsync(string mobile, string message)
        {
            // return "Skip";
            try
            {
                //string source = "PHQ BD";
                //string url = String.Format("http://api.boom-cast.com/boomcast/WebFramework/boomCastWebService/externalApiSendTextMessage.php?masking=NOMASK&userName=OpusTech&password=c3eb7e87b84e252777057a07d984e98e&MsgType=TEXT&receiver={0}&message={1}", mobile, message);
                //string url = String.Format("http://api.rmlconnect.net/bulksms/bulksms?username=OpusSGNMask&password=9VTVB8T8&type=0&dlr=1&destination={0}&source=8809612445509&message={1}", mobile, message);
                string url = String.Format("http://api.rmlconnect.net/bulksms/bulksms?username=OpusSGNMask&password=9VTVB8T8&type=0&dlr=1&destination={0}&source=PHQ%20BD&message={1}", mobile, message);
                //string url = String.Format("http://api.rmlconnect.net/sendsms?username=OpusSGNMask&password=9VTVB8T8&type=0&dlr=0&destination={0}&source={2}&message={1}", mobile, message,source);

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var smsData = await response.Content.ReadAsStringAsync();
                //string[] splData = smsData.Split("|");
                //dynamic data = JsonConvert.DeserializeObject(splData);

                //if (splData[0].ToString() == "1701") return "success";
                return smsData;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
