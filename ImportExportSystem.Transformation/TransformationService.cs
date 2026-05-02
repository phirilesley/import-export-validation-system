using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ImportExportSystem.Transformation
{
    public class TransformationService
    {
        public Dictionary<string, string> TransformRow(Dictionary<string, string> row)
        {
            var transformed = new Dictionary<string, string>(row);

            foreach (var key in transformed.Keys.ToList())
            {
                transformed[key] = TransformValue(transformed[key]);
            }

            return transformed;
        }

        private string TransformValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            // Trim whitespace
            value = value.Trim();

            // Normalize case for names
            if (Regex.IsMatch(value, @"^[a-zA-Z\s]+$"))
            {
                value = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
            }

            // Convert formats if needed (e.g., date normalization)

            return value;
        }
    }
}