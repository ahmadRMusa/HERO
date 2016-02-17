using HERO.Models;
using HERO.Scheduler;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
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

        public static Period calendarPeriod = new Period(DateTime.Now, new DateTime(2100, 1, 1));

        public static DateTime UnixTimestampToDateTime(double unixTimestamp)
        {
            DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            date = date.AddSeconds(unixTimestamp).ToLocalTime();
            return date;
        }

        public static DateTime GetDateTimeFromFullCalendar(string date)
        {
            Regex regex = new Regex(@"^\w{3}\s\w{3}\s\d{1,2}\s\d{4}");
            string[] stringArr = regex.Match(date).Value.Split(' ');
            int month = DateTime.ParseExact(stringArr[1], "MMM", CultureInfo.InvariantCulture).Month;
            int day = Convert.ToInt32(stringArr[2]);
            int year = Convert.ToInt32(stringArr[3]);

            return new DateTime(year, month, day);
        }

        public static void ThrowDetailedEntityValidationErrors(DbEntityValidationException e)
        {
            // Retrieve the error messages as a list of strings.
            var errorMessages = e.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

            // Join the list to a single string.
            var fullErrorMessage = string.Join("; ", errorMessages);

            // Combine the original exception message with the new one.
            var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);

            // Throw a new DbEntityValidationException with the improved exception message.
            throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
        }

        public static string GetEmailBody(string name, string gym, Guid token)
        {
            string head = String.Format("<h1>Welcome to {0}</h1>", gym);
            string body = String.Format("<p>Hey there {0}!</p><p>Thanks for signing up for a HERO CrossFit account! To set up your account details, please click the link below.</p>\n", name);
            string link = String.Format(@"<a href = ""http://localhost:11700/Account/AthleteSignup?token={0}"">Click here to signup!</a>", token);

            return head + body + link;
        }
    }
}