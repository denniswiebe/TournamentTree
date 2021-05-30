using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    public interface ILog
    {
        public void AddEntry(string logEntry);
        public void CreateLog();
    }
}
