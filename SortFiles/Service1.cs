using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace SortFiles
{
    public partial class SortFiles : ServiceBase
    {
        private readonly Timer _tmrExecutor = new Timer();

        public SortFiles()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Init Logs
            Logger.InitLogFile();

            // Init Event
            _tmrExecutor.Elapsed += new ElapsedEventHandler(DoWork);
            _tmrExecutor.Interval = Config.CallEverySeconds * 1000;
            _tmrExecutor.Enabled = true;
            _tmrExecutor.Start();
        }

        protected override void OnStop()
        {
            // Disable Event
            _tmrExecutor.Enabled = false;
        }

        protected void DoWork(object sender, ElapsedEventArgs e)
        {
            try
            {
                // Check source folder
                if (!Directory.Exists(Config.DirectoryPathToMonitor))
                {
                    Logger.LogError($"Folder that should be a source of files does not exist!");
                    return;
                }

                // Check destination folder
                if (!Directory.Exists(Config.DirectoryPathToDestination))
                {
                    Logger.LogError($"Folder that should be a destination for the files does not exist!");
                    return;
                }

                // Check folder for invalid files 
                if (!Directory.Exists(Config.DirectoryPathToInvalidFiles))
                {
                    Logger.LogError($"Folder that should be used for invalid files does not exist!");
                    return;
                }

                // Check amount of work and end the call if there is nothing to do
                var filesToProcess = Directory.GetFiles(Config.DirectoryPathToMonitor);
                if (filesToProcess.Length == 0)
                {
                    Logger.LogError($"Call {DateTime.UtcNow:o}");
                    return;
                }

                // We have to find all valid destination folders
                var allDestinationDirectores = Directory.GetDirectories(Config.DirectoryPathToDestination, "*", SearchOption.AllDirectories);

                Logger.Log($"There are [{filesToProcess.Length}] files to process.");
                foreach (var fileNameWithPath in filesToProcess)
                {
                    try
                    {
                        FileOperations.ProcessSingleFile(fileNameWithPath, allDestinationDirectores);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogCriticalError(ex, $"Error during file PROCESSING has been thrown!");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogCriticalError(ex, $"GENERAL error has been thrown!");
            }
        }
    }
}
