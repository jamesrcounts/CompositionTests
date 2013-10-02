using ApprovalTests;
using ApprovalUtilities.Utilities;
using Microsoft.ComponentModel.Composition.Diagnostics;
using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;

namespace CompositionTests
{
    public static class MefComposition
    {
        public static void VerifyAggregateCatalog(AggregateCatalog catalog)
        {
            VerifyCompositionInfo(
                catalog,
                GetDirectoryCatalogScrubber(catalog),
                StringExtensions.ScrubPublicKeyToken,
                StringExtensions.ScrubVersionNumber);
        }

        public static void VerifyAssemblyCatalog(Assembly assembly)
        {
            VerifyAssemblyCatalog(new AssemblyCatalog(assembly));
        }

        public static void VerifyAssemblyCatalog(string assemblyPath)
        {
            VerifyAssemblyCatalog(new AssemblyCatalog(assemblyPath));
        }

        public static void VerifyAssemblyCatalog(AssemblyCatalog assemblyCatalog)
        {
            VerifyCompositionInfo(
                assemblyCatalog,
                StringExtensions.ScrubVersionNumber,
                StringExtensions.ScrubPublicKeyToken);
        }

        public static void VerifyCatalog<T>(T catalog) where T : ComposablePartCatalog
        {
            var methods = from method in typeof(MefComposition).GetMethods()
                          where method.Name.StartsWith("Verify")
                          let parameters = method.GetParameters()
                          where parameters.Length == 1 && parameters[0].ParameterType == typeof(T)
                          select method;

            if (methods.Count() == 1)
            {
                methods.Single().Invoke(null, new[] { catalog });
                return;
            }

            MefComposition.VerifyCompositionInfo(catalog);
        }

        public static void VerifyCompositionInfo(ComposablePartCatalog catalog, params Func<string, string>[] scrubbers)
        {
            VerifyCompositionInfo(catalog, new CompositionContainer(catalog), scrubbers);
        }

        public static void VerifyCompositionInfo(ComposablePartCatalog catalog, ExportProvider host, params Func<string, string>[] scrubbers)
        {
            VerifyCompositionInfo(() => new CompositionInfo(catalog, host), scrubbers);
        }

        public static void VerifyCompositionInfo(Func<CompositionInfo> getCompositionInfo, params Func<string, string>[] scrubbers)
        {
            try
            {
                var result = OrderedCompositionInfoTextFormatter.Format(getCompositionInfo())
                    .NormalizeLineEndings()
                    .NormalizeIndentation();

                Approvals.Verify(result, ScrubberUtils.Combine(scrubbers));
            }
            catch (ReflectionTypeLoadException ex)
            {
                Array.ForEach(ex.LoaderExceptions, lex => Console.Error.WriteLine(lex.ToString()));
                throw;
            }
        }

        public static void VerifyDirectoryCatalog(string path)
        {
            VerifyDirectoryCatalog(new DirectoryCatalog(path));
        }

        public static void VerifyDirectoryCatalog(string path, string searchPattern)
        {
            VerifyDirectoryCatalog(new DirectoryCatalog(path, searchPattern));
        }

        public static void VerifyDirectoryCatalog(DirectoryCatalog directoryCatalog)
        {
            VerifyCompositionInfo(directoryCatalog, s => s.ScrubPath(directoryCatalog.Path));
        }

        public static void VerifyTypeCatalog(params Type[] types)
        {
            VerifyTypeCatalog(new TypeCatalog(types));
        }

        public static void VerifyTypeCatalog(TypeCatalog typeCatalog)
        {
            VerifyCompositionInfo(typeCatalog);
        }

        private static Func<string, string> GetDirectoryCatalogScrubber(AggregateCatalog catalog)
        {
            var directoryCatalogPaths = from cat in catalog.Catalogs
                                        where cat is DirectoryCatalog
                                        select ((DirectoryCatalog)cat).Path;
            return s =>
            {
                foreach (var path in directoryCatalogPaths)
                {
                    if (path == null)
                    {
                        return null;
                    }

                    s = s.ScrubPath(path);
                }
                return s;
            };
        }
    }
}