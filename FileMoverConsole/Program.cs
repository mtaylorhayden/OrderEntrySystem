using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace FileMoverConsole
{
    class Program
    {
        private static FileSystemWatcher fileWatcher;

        static void Main(string[] args)
        {
            fileWatcher = new FileSystemWatcher("C:\\Starting\\test.txt");

            fileWatcher.EnableRaisingEvents = true;

            fileWatcher.Created += OnCreate;
            fileWatcher.Changed += OnChange;
            fileWatcher.Renamed += OnRename;

            Console.ReadLine();
        }

        private static void OnChange(object sender, FileSystemEventArgs e)
        {
            EventLog eventLog = new EventLog();

            try
            {
                eventLog.Source = "Application";

                eventLog.WriteEntry(" moved successfully!", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                eventLog.Source = "Application";

                eventLog.WriteEntry("File was not moved.", EventLogEntryType.Error);

                Console.WriteLine("The file could not be moved from the starting folder to the destination folder.", ex);
            }
        }

        private static void OnCreate(object sender, FileSystemEventArgs e)
        {
            //Directory.CreateDirectory("C:\\Destination");
            Thread.Sleep(1000);

            try
            {
                if (File.Exists("C:\\Starting\\test.txt") == true)
                {
                    File.Delete("C:\\Starting\\test.txt");
                }
                else
                {
                    Console.WriteLine("File does not exist. Could not delete.");
                }

                // Source to destination.
                File.Move("C:\\Starting\\test.txt", "C:\\TargetDestination");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


        }

        private static void OnRename(object sender, FileSystemEventArgs e)
        {

        }
    }
}
