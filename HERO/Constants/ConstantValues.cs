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
            return String.Format("<h1>Welcome to {0}</h1><p>Hey there {1}!</p><p>Thanks for signing up for a HERO CrossFit account! To set up your account details, <a href=\"www.google.com/{2}\">please click here</a>.</p>", gym, name, token.ToString());
        }
    }
}