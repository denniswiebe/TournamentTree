using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    public interface IExcelExport
    {
        public void Export(SpreadsheetDocument document, SheetData sheetData, WorksheetPart worksheetPart);
    }
}
