using System.Text.RegularExpressions;

namespace Common.Extension
{
    public static class StringExtension
    {
        public static string OnlyNumbers(this string value)
        {
            return Regex.Replace(value, "[^0-9]", "");
        }
    }
}