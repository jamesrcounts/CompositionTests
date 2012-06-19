using System;
using System.Text.RegularExpressions;

namespace CompositionTests
{
    public static class StringExtensions
    {
        public static string NormalizeLineFeeds(this string value)
        {
            return new Regex(@"\r\n?|\n").Replace(value, Environment.NewLine);
        }

        public static string NormalizeWhitespace(this string value)
        {
            return value.Replace("\t", new string(' ', 4));
        }

        public static string ScrubPublicKeyToken(this string value)
        {
            if (value == null)
            {
                return null;
            }

            return new Regex(@",\sPublicKeyToken=[\dabcdefnul]+").Replace(value, string.Empty);
        }

        public static string ScrubVersionNumber(this string value)
        {
            if (value == null)
            {
                return null;
            }

            return new Regex(@",\sVersion=[\d.]+").Replace(value, string.Empty);
        }
    }
}