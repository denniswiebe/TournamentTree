using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    /// <summary>
    /// Diese Klasse kümmert sich um das Anlegen von Zeilen in einer Excel-Datei
    /// </summary>
    public static class RowsCreator
    {
        public static void CreateRows(SheetData sheetData, int rowsToCreate)
        {
            for (int i = 1; i <= rowsToCreate; i++)
            {
                var row = new Row { RowIndex = (uint)i };
                sheetData.AppendChild(row);
            }
        }
    }
}
