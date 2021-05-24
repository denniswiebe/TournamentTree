using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentTree
{
    public static class CellFinder
    {
        public static Cell GetCell(Worksheet worksheet, int columnId, int rowIndex)
        {
            var columnName = GetLetterByNumber(columnId);
            Row row = GetRow(worksheet, rowIndex);

            if (row == null)
                return null;

            var cell = row.Elements<Cell>().FirstOrDefault(c => string.Compare(c.CellReference.Value, columnName + rowIndex, true) == 0);
            if (cell == null)
            {
                cell = new Cell { CellReference = new StringValue(columnName + rowIndex) };
                row.AppendChild(cell);
                worksheet.Save();
            }

            return cell;
        }

        private static Row GetRow(Worksheet worksheet, int rowIndex)
        {
            var sheetData = worksheet.GetFirstChild<SheetData>();
            var row = sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex == rowIndex);
            return row;
        }

        /// <summary>
        /// Diese Methode gibt ausgehend von einer Zahl den passenden Buchstaben zurück
        /// Also: 1 = A, 2 = B, 3 = C, ...
        /// </summary>
        /// <param name="number">Um welche Spalte handelt es sich?</param>
        /// <returns></returns>
        public static string GetLetterByNumber(int number)
        {
            // Da der Buchstabe A den ASCII-Code 65 hat, muss der Parameter der Methode mit 64 addiert werden,
            // sodass der korrekte Buchstabe ermittelt werden kann.
            var asciiNumber = number + 64;

            // Ist der Wert von asciiNumber nun nicht im Bereich der 26 Buchstaben
            // wird eine Exception geworfen, da dies nicht möglich sein kann
            // und es sich somit um einen Fehler im Code handelt.
            if (asciiNumber < 65 || asciiNumber > 90)
                throw new ApplicationException("number represents no letter from the alphabet");

            return ((char)asciiNumber).ToString();
        }
    }
}
