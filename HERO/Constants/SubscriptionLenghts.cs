using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HERO.Constants
{
    public static class SubscriptionLenghts
    {
        public static Dictionary<string, int> SubscriptionLengthOptions = new Dictionary<string, int>()
        {
            { "1 Month", 1 },
            { "6 Months", 6 },
            { "12 Months", 12 }
        };
    }
}