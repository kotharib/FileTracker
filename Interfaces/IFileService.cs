using FileTracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTracker.Interfaces
{
    interface IFileService
    {
        bool IsFolderExists();
        bool IsFolderAccessible();
        int GetNoOfFiles();
        int GetNoOfFolders();
        int GetNoofFoldersInaccessible();
        decimal GetTotalSize(SizeFormat sizeFormat);
        decimal GetSizeOfFilesModifiedByYears(int noOfYears, SizeFormat sizeFormat);
    }
}
