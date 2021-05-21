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
        /// Komische Methode, die aus einer Zahl einen Buchstaben zurückgibt.
        /// Weiß nicht, ob Excel beispielsweise aus 11 automatisch A1 macht, 
        /// das muss noch weiter untersucht werden.
        /// </summary>
        /// <param name="number">Um welche Spalte handelt es sich?</param>
        /// <returns></returns>
        public static string GetLetterByNumber(int number)
        {
            switch (number)
            {
                case 1:
                    return "A";
                case 2:
                    return "B";
                case 3:
                    return "C";
                case 4:
                    return "D";
                case 5:
                    return "E";
                case 6:
                    return "F";
                case 7:
                    return "G";
                case 8:
                    return "H";
                case 9:
                    return "I";
                case 10:
                    return "J";
                case 11:
                    return "K";
                case 12:
                    return "L";
                case 13:
                    return "M";
                case 14:
                    return "N";
                case 15:
                    return "O";
                case 16:
                    return "P";
                case 17:
                    return "Q";
                case 18:
                    return "R";
                case 19:
                    return "S";
                case 20:
                    return "T";
                case 21:
                    return "U";
                case 22:
                    return "V";
                case 23:
                    return "W";
                case 24:
                    return "X";
                case 25:
                    return "Y";
                case 26:
                    return "Z";
                default:
                    return "UNKNOWN";
            }
        }
    }
}
