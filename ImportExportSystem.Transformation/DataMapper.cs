using System.Collections.Generic;

namespace ImportExportSystem.Transformation
{
    public class DataMapper
    {
        public Dictionary<string, string> MapRow(Dictionary<string, string> row, Dictionary<string, string> mapping)
        {
            var mappedRow = new Dictionary<string, string>();

            foreach (var map in mapping)
            {
                if (row.ContainsKey(map.Key))
                {
                    mappedRow[map.Value] = row[map.Key];
                }
            }

            return mappedRow;
        }
    }
}