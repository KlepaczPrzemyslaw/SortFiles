namespace SortFiles
{
    public static class DirectoryNameExtractor
    {
        public static string ExtractDirectoryNameFromFileName(string fileName)
        {
            int indexOfUnderscore = fileName.IndexOf('_');
            if (indexOfUnderscore == -1)
            {
                Logger.Log($"This file [{fileName}] does not contain '_' character, that is required!");
                return null;
            }

            return Config.ShouldUnderscoreBeIncludedInDirectoryName
                // It will return string and plus 1 character (Exacly '_')
                ? fileName.Substring(0, indexOfUnderscore + 1)
                // It will cut the string right before '_'
                : fileName.Substring(0, indexOfUnderscore);
        }
    }
}
