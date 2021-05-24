using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    /// <summary>
    /// Diese Klasse ist für den Excel-Export zuständig.
    /// </summary>
    public class ExcelExporter
    {
        public static void ExportToExcel(bool doubleKo = false)
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
    }
}
