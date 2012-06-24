using System;
using System.Text.RegularExpressions;

namespace CompositionTests
{
    public static class StringExtensions
    {
        public static string NormalizeIndentation(this string value)
        {
            if (value == null)
            {
                return null;
            }

            return value.Replace("\t", new string(' ', 4));
        }

        public static string NormalizeLineEndings(this string value)
        {
            if (value == null)
            {
                return null;
            }

            return new Regex(@"\r\n?|\n").Replace(value, Environment.NewLine);
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