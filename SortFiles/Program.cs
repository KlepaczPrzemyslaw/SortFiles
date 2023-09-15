using System.ServiceProcess;

namespace SortFiles
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new SortFiles()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
