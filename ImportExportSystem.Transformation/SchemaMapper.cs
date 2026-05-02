using System.Collections.Generic;

namespace ImportExportSystem.Transformation
{
    public class SchemaMapper
    {
        public Dictionary<string, string> GetDefaultMapping()
        {
            return new Dictionary<string, string>
            {
                { "Full Name", "Name" },
                { "Email Address", "Email" },
                { "Age", "Age" }
            };
        }

        public Dictionary<string, string> MapSchema(Dictionary<string, string> sourceHeaders, Dictionary<string, string> targetFields)
        {
            var mapping = new Dictionary<string, string>();

            foreach (var header in sourceHeaders)
            {
                if (targetFields.ContainsKey(header.Value))
                {
                    mapping[header.Key] = targetFields[header.Value];
                }
            }

            return mapping;
        }
    }
}