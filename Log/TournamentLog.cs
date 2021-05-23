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

        public void ExportToExcel(bool doubleKo = false)
        {
            // Neues Dokument erstellen
            var fileName = "KnockOutStage.xlsx";
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                Sheets sheets = document.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                // Groups
                WorksheetPart worksheetPart1 = workbookPart.AddNewPart<WorksheetPart>();
                SheetData sheetData1 = new SheetData();
                if (TournamentGroupLog.Groups.Count > 0)
                {
                    Worksheet worksheet1 = new Worksheet();
                    worksheet1.AppendChild(sheetData1);
                    worksheetPart1.Worksheet = worksheet1;
                    Sheet sheet1 = new Sheet()
                    {
                        Id = document.WorkbookPart.GetIdOfPart(worksheetPart1),
                        SheetId = 1,
                        Name = "Groups"
                    };
                    sheets.Append(sheet1);
                }

                // KO
                WorksheetPart worksheetPart2 = workbookPart.AddNewPart<WorksheetPart>();
                Worksheet worksheet2 = new Worksheet();
                SheetData sheetData2 = new SheetData();
                worksheet2.AppendChild(sheetData2);
                worksheetPart2.Worksheet = worksheet2;
                Sheet sheet2 = new Sheet()
                {
                    Id = document.WorkbookPart.GetIdOfPart(worksheetPart2),
                    SheetId = 2,
                    Name = "KO"
                };
                sheets.Append(sheet2);

                if (TournamentGroupLog.Groups.Count > 0)
                    TournamentGroupLog.GenerateGroupExcel(document, sheetData1, worksheetPart1);
                if (!doubleKo)
                    TournamentBracketLog.GenerateBracketExcel(document, sheetData2, worksheetPart2);
                else
                    TournamentDoubleKoLog.GenerateBracketExcel(document, sheetData2, worksheetPart2);
                document.Close();
            }
        }

        public static Cell GetCell(Worksheet worksheet, string columnName, int rowIndex)
        {
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

        public static Row GetRow(Worksheet worksheet, int rowIndex)
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

        public static void WriteValueInCell(Worksheet worksheet, CellValues cellValue, string text, int column, int row)
        {
            Cell cell = GetCell(worksheet, GetLetterByNumber(column), row);
            cell.CellValue = new CellValue(text);
            cell.DataType = new EnumValue<CellValues>(cellValue);
        }
    }
}
