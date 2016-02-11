using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HERO.Constants
{
    public static class ConstantValues
    {
        public static Dictionary<string, int> SubscriptionLengthOptions = new Dictionary<string, int>()
        {
            { "1 Month", 1 },
            { "6 Months", 6 },
            { "12 Months", 12 }
        };

        public static string GetEmailBody(string name, string gym, Guid token)
        {
            string head = String.Format("<h1>Welcome to {0}</h1>", gym);
            string body = String.Format("<p>Hey there {0}!</p><p>Thanks for signing up for a HERO CrossFit account! To set up your account details, please click the link below.</p>\n", name);
            string link = String.Format(@"<a href = ""http://localhost:11700/Account/AthleteSignup?token={0}"">Click here to signup!</a>", token);

            return head + body + link;
        }
    }
}