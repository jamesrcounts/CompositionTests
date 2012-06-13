using System;
using System.Text.RegularExpressions;

namespace CompositionTests
{
    public static class StringExtensions
    {
        public static string ScrubVersionNumber(this string value)
        {
            if (value == null)
            {
                return null;
            }

            return new Regex(@",\sVersion=[\d.]+").Replace(value, string.Empty);
        }

        public static string ScrubPublicKeyToken(this string value)
        {
            if (value == null)
            {
                return null;
            }

            return new Regex(@",\sPublicKeyToken=[\dabcdefnul]+").Replace(value, string.Empty);
        }
    }
}