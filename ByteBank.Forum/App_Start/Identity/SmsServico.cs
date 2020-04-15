using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ByteBank.Forum.App_Start.Identity
{
    public class SmsServico : IIdentityMessageService
    {
        private readonly string TWILIO_USERNAME = ConfigurationManager.AppSettings["twilio:username"];
        private readonly string TWILIO_PASSWORD = ConfigurationManager.AppSettings["twilio:password"];
        private readonly string TWILIO_FROM_NUMBER = ConfigurationManager.AppSettings["twilio:from_number"];
        public async Task SendAsync(IdentityMessage message)
        {
            TwilioClient.Init(TWILIO_USERNAME, TWILIO_PASSWORD);
            await MessageResource.CreateAsync(new PhoneNumber(message.Destination), from: TWILIO_FROM_NUMBER, body: message.Body);
        }
    }
}