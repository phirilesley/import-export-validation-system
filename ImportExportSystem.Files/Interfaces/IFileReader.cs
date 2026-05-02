using System.Collections.Generic;

namespace ImportExportSystem.Files.Interfaces
{
    public interface IFileReader
    {
        IEnumerable<Dictionary<string, string>> ReadFile(string filePath);
    }
}