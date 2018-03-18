using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTracker.Interfaces
{
    public interface ICsvWriter
    {
        void WriteRow(List<string> columns, string delimiter);
        void DeleteifExists();
        bool IsFileinUse();
        bool IsValidCsvFile();
    }
}
