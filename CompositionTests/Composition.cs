using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Reflection;
using ApprovalTests;
using Microsoft.ComponentModel.Composition.Diagnostics;

namespace CompositionTests
{
    public class Composition
    {
        public static void DiscoverParts(ComposablePartCatalog catalog, params Func<string, string>[] scrubbers)
        {
            DiscoverParts(catalog, new CompositionContainer(catalog), scrubbers);
        }

        public static void DiscoverParts(ComposablePartCatalog catalog, ExportProvider host, params Func<string, string>[] scrubbers)
        {
            DiscoverParts(() => new CompositionInfo(catalog, host), scrubbers);
        }

        public static void DiscoverParts(Func<CompositionInfo> getCompositionInfo, params Func<string, string>[] scrubbers)
        {
            try
            {
                using (var stringWriter = new StringWriter())
                {
                    OrderedCompositionInfoTextFormatter.Write(getCompositionInfo(), stringWriter);
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
    }
}