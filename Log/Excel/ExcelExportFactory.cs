using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    public class ExcelExportFactory
    {
        public IExcelExport CreateExcelExport(ExcelExportType type)
        {
            if (type == ExcelExportType.Bracket)
                return new TournamentBracketLog();
            if (type == ExcelExportType.DoubleKo)
                return new TournamentDoubleKoLog();
            if (type == ExcelExportType.Groups)
                return new TournamentGroupLog();

            return null;
        }

        public enum ExcelExportType
        {
            None = 0,
            Bracket = 1,
            DoubleKo = 2,
            Groups = 3
        }
    }
}
