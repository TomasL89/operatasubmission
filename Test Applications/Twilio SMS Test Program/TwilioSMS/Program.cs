using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace TwilioSMS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string accountSid = "your secrete ";
            const string authToken = "your secret token";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "There is a new Log File",
                from: new Twilio.Types.PhoneNumber("+123456789"),
                to: new Twilio.Types.PhoneNumber("+123456789")
            );

            Console.WriteLine(message.Sid);
        }
    }
}