using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LikeBotService.AddFun
{
    public static class StringExtension
    {
        public static decimal getDecimal(this string str)
        {
            return Convert.ToDecimal(str);
        }

        public static int getInteger(this string str)
        {
            int value = 0;
            foreach (char c in str)
            {
                if ((c >= '0') && (c <= '9'))
                {
                    value = value * 10 + (c - '0');
                }
            }
            return value;
        }
        public static string getDecimalstr(this string str)
        {
            return decimal.Parse(str).ToString();
        }

        public static string findStringRegExGroup(this string str, string regular, string group)
        {
            Regex regex = new Regex(regular);
            Match match = regex.Match(str);
            return match.Groups[group].Value;
        }
        public static string findStringRegEx(this string str, string regular)
        {
            Regex regex = new Regex(regular);
            Match match = regex.Match(str);
            return match.Value;
        }
    }
    public static class ParseFunc
    {
    }
}
