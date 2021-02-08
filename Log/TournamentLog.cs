using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TournamentTree
{
    class TournamentLog
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
