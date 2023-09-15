using System;
using System.IO;
using System.Linq;

namespace SortFiles
{
    public static class FileOperations
    {
        private static string FileNameDate => $"{DateTime.UtcNow:yyyy-MM-dd-HH-mm-fffffff}-UTC";

        public static void ProcessSingleFile(string fileNameWithPath, string[] foundDestinationDirectores)
        {
            // Get only name of the file
            var splitedFilePath = fileNameWithPath.Split('\\');
            var onlyFileName = splitedFilePath[splitedFilePath.Length - 1];

            try
            {
                // Extract first part of name (part before '_'), then if file name does not have '_' instantly move to invalid
                var destinationFolderName = DirectoryNameExtractor.ExtractDirectoryNameFromFileName(onlyFileName);
                if (destinationFolderName == null)
                {
                    MoveFileToInvalidDirectory(fileNameWithPath, onlyFileName);
                    return;
                }

                // If file has '_', then we have to find proper folder for it
                var destinationFolderPath = foundDestinationDirectores.FirstOrDefault(x => x.EndsWith(destinationFolderName));

                // Folder not found -> move to invalid
                if (destinationFolderPath == null)
                {
                    Logger.LogError($"There is no folder that matches required [{destinationFolderName}] for file [{onlyFileName}]");
                    MoveFileToInvalidDirectory(fileNameWithPath, onlyFileName);
                }
                // Folder found -> move to it
                else
                {
                    MoveFileToProperDirectory(fileNameWithPath, destinationFolderPath, onlyFileName);
                }
            }
            catch (PathTooLongException)
            {
                File.Move(fileNameWithPath, $"{Config.DirectoryPathToInvalidFiles}\\error_{FileNameDate}_FILE---{onlyFileName}");
                Logger.LogError($"File with name [{onlyFileName}] cannot be copied! Destination path is too long! (Too many Sub-Folders)!");
            }
            catch (UnauthorizedAccessException)
            {
                File.Move(fileNameWithPath, $"{Config.DirectoryPathToInvalidFiles}\\error_{FileNameDate}_FILE---{onlyFileName}");
                Logger.LogError($"File with name [{onlyFileName}] cannot be copied! There is issue with permissions!");
            }
            catch (FileNotFoundException)
            {
                Logger.LogError($"File with name [{onlyFileName}] with path [{fileNameWithPath}] for some reason does not exist! Contact developer!");
            }
            catch (DirectoryNotFoundException)
            {
                File.Move(fileNameWithPath, $"{Config.DirectoryPathToInvalidFiles}\\error_{FileNameDate}_FILE---{onlyFileName}");
                Logger.LogError($"File with name [{onlyFileName}] cannot be copied, because destination folder for some reason does not exist! Contact developer!");
            }
            catch (NotSupportedException)
            {
                Logger.LogError($"File with name [{onlyFileName}] is in inproper format! (NotSupportedException has been thrown)!");
            }
            catch (IOException)
            {
                File.Move(fileNameWithPath, $"{Config.DirectoryPathToInvalidFiles}\\error_{FileNameDate}_FILE---{onlyFileName}");
                Logger.LogError($"File with name [{onlyFileName}] cannot be copied! File with the same name already exists inside destination folder!");
            }
        }

        private static void MoveFileToInvalidDirectory(string fileNameWithPath, string onlyFileName)
        {
            File.Move(fileNameWithPath, $"{Config.DirectoryPathToInvalidFiles}\\{onlyFileName}");
            Logger.LogError($"[{onlyFileName}] has been moved to directory with invalid files!");
        }

        private static void MoveFileToProperDirectory(string fileNameWithPath, string pathToProperDirectory, string onlyFileName)
        {
            File.Move(fileNameWithPath, $"{pathToProperDirectory}\\{onlyFileName}");
            Logger.Log($"File [{fileNameWithPath}] has been moved to directory [{pathToProperDirectory}].");
        }
    }
}
