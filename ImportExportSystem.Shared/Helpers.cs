using System;

namespace ImportExportSystem.Shared
{
    public static class Helpers
    {
        public static string GenerateFileId() => Guid.NewGuid().ToString();

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static string SanitizeString(string input)
        {
            return input?.Trim() ?? string.Empty;
        }
    }
}