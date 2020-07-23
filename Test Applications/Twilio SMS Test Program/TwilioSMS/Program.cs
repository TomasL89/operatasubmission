using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace TwilioSMS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Find your Account Sid and Token at twilio.com/console
            // DANGER! This is insecure. See http://twil.io/secure
            const string accountSid = "AC5cd9bd1e9026ea93f03d536d2d70543d";
            const string authToken = "c4ef466abbfa55855d0025eca5e86c0c";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "This is a test message for that potential employer?",
                from: new Twilio.Types.PhoneNumber("+12055707370"),
                to: new Twilio.Types.PhoneNumber("+61435911879")
            );

            Console.WriteLine(message.Sid);
        }
    }
}