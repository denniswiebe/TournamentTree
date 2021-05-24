using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Linq;

namespace TournamentTree
{
    public class TournamentLog
    {
        public List<String> LogEntries { get; set; } = new List<string>();

        public void AddEntry(String logEntry)
        {
            LogEntries.Add(logEntry);
            LogEntries.Add("\n");
        }

        public void CreateLog()
        {
            try
            {
                StreamWriter sw = new StreamWriter("log.txt");
                foreach (String entry in LogEntries)
                {
                    sw.WriteLine(entry);
                }
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
