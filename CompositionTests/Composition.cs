using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using ApprovalTests;
using Microsoft.ComponentModel.Composition.Diagnostics;

namespace CompositionTests
{
    public class Composition
    {
        public static void DiscoverParts(Func<CompositionInfo> getCompositionInfo, params Func<string, string>[] scrubbers)
        {
            try
            {
                using (var stringWriter = new StringWriter())
                {
                    CompositionInfoTextFormatter.Write(getCompositionInfo(), stringWriter);
                    var result = stringWriter.ToString();
                    foreach (var scrubber in scrubbers)
                    {
                        result = scrubber(result);
                    }

                    Approvals.Verify(result);
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                Array.ForEach(ex.LoaderExceptions, lex => Console.Error.WriteLine(lex.ToString()));
                throw;
            }
        }

        public static string ScrubPublicKeyToken(string value)
        {
            return new Regex(@",\sPublicKeyToken=[\dabcdefnul]+").Replace(value, string.Empty);
        }

        public static string ScrubVersionNumber(string value)
        {
            return new Regex(@",\sVersion=[\d.]+").Replace(value, string.Empty);
        }
    }
}