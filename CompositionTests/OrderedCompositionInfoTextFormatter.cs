using System.IO;
using System.Linq;
using Microsoft.ComponentModel.Composition.Diagnostics;

namespace CompositionTests
{
    public static class OrderedCompositionInfoTextFormatter
    {
        public static string Format(CompositionInfo info)
        {
            using (var output = new StringWriter())
            {
                Write(info, output);
                return output.ToString();
            }
        }

        public static void Write(CompositionInfo info, TextWriter output)
        {
            var partDefinitions = info.PartDefinitions;
            if (partDefinitions == null)
            {
                return;
            }

            var orderedDefinitions = partDefinitions.OrderBy(GetPartDefinitionKey);

            foreach (var part in orderedDefinitions)
            {
                PartDefinitionInfoTextFormatter.Write(part, output);
                output.WriteLine();
            }
        }

        private static string GetPartDefinitionKey(PartDefinitionInfo pd)
        {
            return CompositionElementTextFormatter.DisplayCompositionElement(pd.PartDefinition);
        }
    }
}