using FileTracker.Enums;
using FileTracker.Interfaces;
using FileTracker.Services;
using FileTracker.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter a folder and csv file path.");
                Console.WriteLine("Usage: GetFileStatistics <folder_path> <csv_file_path>");
                Console.Read();
                return;
            }

            string folderpath = args[0];
            string csvPath = args[1];
            IFileService fileService = new FileService(folderpath);
            ICsvWriter csvWriter = new CsvWriter(csvPath);

            if (!fileService.IsFolderExists()) {
                Console.WriteLine("The folder path seems to be invalid or does not exist.");
                Console.Read();
                return;
            }

            if (!fileService.IsFolderAccessible())
            {
                Console.WriteLine("The folder path seems to be inaccessible or you dont have adequate permissions.");
                Console.Read();
                return;
            }

            if (!csvWriter.IsValidCsvFile())
            {
                Console.WriteLine("The Output file is not a valid csv filename");
                Console.Read();
                return;
            }

            if (csvWriter.IsFileinUse())
            {
                Console.WriteLine("The target file is under use or under process.");
                Console.Read();
                return;
            }

            //Delete the file if already exists
            csvWriter.DeleteifExists();
            
            string delimiter = ",";
            List<string> headings = new List<string>() {
                "Folder",
                "Total Size(GB)",
                "Modified < 1 Year Ago (GB)",
                "Modified < 3 Years Ago (GB)",
                "Modified < 5 Years Ago (GB)",
                "No of Inaccessible Folders",
                "No of Files",
                "No of Folders"
            };
            csvWriter.WriteRow(headings, delimiter);


            List<string> values = new List<string>() {
                folderpath,
                fileService.GetTotalSize(SizeFormat.GB).ToString("#.#"),
                fileService.GetSizeOfFilesModifiedByYears(1, SizeFormat.GB).ToString("#.#"),
                fileService.GetSizeOfFilesModifiedByYears(3, SizeFormat.GB).ToString("#.#"),
                fileService.GetSizeOfFilesModifiedByYears(5, SizeFormat.GB).ToString("#.#"),
                fileService.GetNoofFoldersInaccessible().ToString(),
                fileService.GetNoOfFiles().ToString(),
                fileService.GetNoOfFolders().ToString()
            };            

            csvWriter.WriteRow(values, delimiter);


            Console.WriteLine("The Statistics for {0} is successfully written at {1}", args[0],args[1]);
            Console.WriteLine("Press any key to exit.");
            Console.Read();
        }
    }
}
