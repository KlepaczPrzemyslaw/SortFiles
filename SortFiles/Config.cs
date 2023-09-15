namespace SortFiles
{
    public static class Config
    {
        // The files should be inserted inside this directory
        public static string DirectoryPathToMonitor => "C:\\InsertPathToSourceFolderHere";

        // This path should lead to main folder, that should be a destination
        public static string DirectoryPathToDestination => "C:\\InsertPathToDestinationFolderHere";

        // This path will be used when file does not have '_' or there is no matching folder 
        public static string DirectoryPathToInvalidFiles => "C:\\InsertPathToFolderForInvalidFilesHere";

        // If true program will search for folder with name "123_" | if false will search for "123"
        public static bool ShouldUnderscoreBeIncludedInDirectoryName => false;

        // "30" here will force progream to run task every 30 sec.
        public static int CallEverySeconds => 30;
    }
}
