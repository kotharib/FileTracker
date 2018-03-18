using FileTracker.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTracker.Utilities
{
    public class CsvWriter : ICsvWriter
    {
        private string _csvFilePath;

        public CsvWriter(string csvFilePath)
        {
            _csvFilePath = csvFilePath;
        }

        /// <summary>
        /// check if file is in use
        /// </summary>
        /// <returns></returns>
        public bool IsFileinUse()
        {
            FileInfo file = new FileInfo(_csvFilePath);
            FileStream stream = null;
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {   
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false;
        }

        /// <summary>
        /// delete the file if already exists
        /// </summary>
        public void DeleteifExists()
        {
            if (File.Exists(_csvFilePath)) {
                File.Delete(_csvFilePath);
            }
        }


        public bool IsValidCsvFile()
        {
            FileInfo file = new FileInfo(_csvFilePath);
            return file.Extension == ".csv";
        }
        
        /// <summary>
        /// write a row to the file 
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="delimiter"></param>
            public void WriteRow(List<string> columns, string delimiter)
        {
            if (columns == null)
                throw new ArgumentNullException("columns");

            StringBuilder sbLine = new StringBuilder();
            columns.ForEach(x=>
            {
                sbLine.Append(x);
                sbLine.Append(delimiter);
            });
            sbLine.Remove(sbLine.Length - 1, 1);
            using (FileStream fs = new FileStream(_csvFilePath, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(sbLine.ToString());
                }
            }
        }
    }

    
}
