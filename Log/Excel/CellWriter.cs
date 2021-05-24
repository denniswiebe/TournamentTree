using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    public static class CellWriter
    {
        public static void WriteValueInCell(Worksheet worksheet, CellValues cellValue, string text, int column, int row)
        {
            Cell cell = CellFinder.GetCell(worksheet, column, row);
            cell.CellValue = new CellValue(text);
            cell.DataType = new EnumValue<CellValues>(cellValue);
        }
    }
}
