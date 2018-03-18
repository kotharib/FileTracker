using FileTracker.Enums;
using FileTracker.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FileTracker.Services
{
    public class FileService : IFileService
    {
        private string _path;

        public FileService(string path)
        {
            _path = path;
        }


        /// <summary>
        /// check if the folder is accessible
        /// </summary>
        /// <returns>bool</returns>
        private bool HasFolderAccess()
        {
            bool readAllow = false;
            bool readDeny = false;
            var accessControlList = Directory.GetAccessControl(_path);
            if (accessControlList == null)
                return false;
            var accessRules = accessControlList.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
            if (accessRules == null)
                return false;
            foreach (FileSystemAccessRule rule in accessRules)
            {
                if ((FileSystemRights.Read & rule.FileSystemRights) != FileSystemRights.Read) continue;

                if (rule.AccessControlType == AccessControlType.Allow)
                    readAllow = true;
                else if (rule.AccessControlType == AccessControlType.Deny)
                    readDeny = true;
            }
            return readAllow && !readDeny;
        }

        /// <summary>
        /// check if file is accessible
        /// </summary>
        /// <returns>bool</returns>
        private bool HasFileAccess()
        {
            bool readAllow = false;
            bool readDeny = false;
            var accessControlList = File.GetAccessControl(_path);
            if (accessControlList == null)
                return false;
            var accessRules = accessControlList.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
            if (accessRules == null)
                return false;
            foreach (FileSystemAccessRule rule in accessRules)
            {
                if ((FileSystemRights.Read & rule.FileSystemRights) != FileSystemRights.Read) continue;

                if (rule.AccessControlType == AccessControlType.Allow)
                    readAllow = true;
                else if (rule.AccessControlType == AccessControlType.Deny)
                    readDeny = true;
            }
            return readAllow && !readDeny;
        }


        public bool IsFolderExists() {
            return Directory.Exists(this._path);
        }

        public bool IsFolderAccessible()
        {
            return HasFolderAccess();
        }
        
        /// <summary>
        /// Get the total number of files
        /// </summary>
        /// <returns>int</returns>
        public int GetNoOfFiles()
        {
            return (Directory.Exists(_path)) ? Directory.GetFiles(_path).Length : throw new DirectoryNotFoundException(); ;
        }

        /// <summary>
        /// Get the number of folders
        /// </summary>
        /// <returns>int</returns>
        public int GetNoOfFolders()
        {
            return (Directory.Exists(_path)) ? Directory.GetDirectories(_path).Length : throw new DirectoryNotFoundException(); ;
        }

        /// <summary>
        /// Get the number of inaccessible folders
        /// </summary>
        /// <returns></returns>
        public int GetNoofFoldersInaccessible()
        {
            int count = 0;
            string[] directories = Directory.GetDirectories(_path);
            foreach (var dir in directories)
            {
                if (!HasFolderAccess()) count++;
            }
            return count;
        }

        /// <summary>
        /// Get the total size of the folder
        /// </summary>
        /// <param name="sizeFormat"></param>
        /// <returns></returns>
        public decimal GetTotalSize(SizeFormat sizeFormat)
        {
            decimal size = 0;
            string[] files = Directory.GetFiles(_path,"*.*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                FileInfo fi = new FileInfo(file);
                size += fi.Length;
            }
            return Decimal.Divide(size, (decimal)sizeFormat); 
        }

        /// <summary>
        /// Get the total size of modified files
        /// </summary>
        /// <param name="noOfYears"></param>
        /// <param name="sizeFormat"></param>
        /// <returns></returns>
        public decimal GetSizeOfFilesModifiedByYears(int noOfYears, SizeFormat sizeFormat)
        {
            decimal size = 0;
            string[] files = Directory.GetFiles(_path, "*.*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                FileInfo fi = new FileInfo(file);
                TimeSpan timeSpan = DateTime.Now - fi.LastWriteTime;
                decimal years = Convert.ToDecimal(timeSpan.TotalDays / 365.25);
                if(years <  noOfYears)
                    size += fi.Length;
            }
            return Decimal.Divide(size, (decimal)sizeFormat);
        }
    }
}
