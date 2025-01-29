using System.Globalization;
using System.Text.RegularExpressions;

namespace ActiveDirectoryExplorer.Extensions
{
    public static class StringExtensions
    {
        public static string BuildDateString(this string datestr)
        {
            datestr = datestr.Replace(".0Z", "");

            DateTime dateTime = DateTime.ParseExact(datestr, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

            return dateTime.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public static string RemoveDNData(this string distinguishedName)
        {
            string newStr = Regex.Replace(distinguishedName, @",.*", "");

            return newStr.Replace("CN=", "");
        }

        public static string[] BuildGroupArray(this string[] groupArray)
        {
            return groupArray?.Select(member => RemoveDNData(member.ToString())).ToArray()
                   ?? [];
        }
    }
}
