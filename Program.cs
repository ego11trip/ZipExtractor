using System;
using System.IO;
using System.IO.Compression;


namespace ZipExtractorApp
{
    class Program
    {
        private static string CURRENT_PATH = Directory.GetCurrentDirectory();
        private const string PATH_SEPARATOR = "\\";
        private const string OPERATION_FAILED = "Uups... Sorry, mate.\nSomething went wrong.";
        private const string NO_FILES_MESSAGE = "No zip files in this directory.";

        static void Main()
        {
            Console.Title = "ZipExtractorApp";
            PimpMyWriting.ApplyColor(CURRENT_PATH, ConsoleColor.Yellow);
            Console.WriteLine();

            var files = GetZipFilesInCurrentDirectory(CURRENT_PATH);

            string fileName = files[0];
            string zipDir = CURRENT_PATH + PATH_SEPARATOR + fileName;
            //
            //
            //
            var startExtraction = DateTime.Now;
            Extraction(zipDir);
            var finishExtraction = DateTime.Now;

            var timePassed = TimePassed(startExtraction, finishExtraction);
            Console.WriteLine($"Time passed: {timePassed}");
            Console.ReadLine();
        }
        

        //
        private static TimeSpan TimePassed(DateTime s, DateTime f)
        {
            var time = f -s;
            return time;
        }

        /// <summary>
        /// Displays available zip files in current directory.
        /// Program terminates if no zip files are found.
        /// </summary>
        /// <param name="path"></param>
        private static string[] GetZipFilesInCurrentDirectory(string path)
        {
            var files = Directory.GetFiles(path, "*.zip");
            if (files.Length == 0)
            {
                PimpMyWriting.ApplyColor(NO_FILES_MESSAGE, ConsoleColor.Cyan);
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
                Environment.Exit(0);
            }

            for (int i = 0; i < files.Length; i++)
            {
                files[i] = Path.GetFileName(files[i]);
            }
            return files;
        }

        private static void Extraction(string filePathToExtract)
        {
            bool iSExtractionSuccessful = false;
            string extractionDir = Path.GetFileNameWithoutExtension(filePathToExtract);

            try
            {
                Console.WriteLine("Starting unpacking . . .");
                ZipFile.ExtractToDirectory(filePathToExtract, extractionDir);
                Console.WriteLine("Everything should be fine!");
                iSExtractionSuccessful = true;
            }

            catch (Exception)
            {
                PimpMyWriting.ApplyColor(OPERATION_FAILED,
                    ConsoleColor.Red);
            }

            finally
            {
                if (iSExtractionSuccessful)
                    File.Delete(filePathToExtract);
            }
        }
    }
}
