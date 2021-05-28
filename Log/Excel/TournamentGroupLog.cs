using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    public class TournamentGroupLog : IExcelExport
    {
        public static List<TournamentGroupLogMatch> AllMatches { get; } = new List<TournamentGroupLogMatch>();
        public static Dictionary<int, List<Player>> Groups { get; } = new Dictionary<int, List<Player>>();

        public static void AddMatch(TournamentGroupLogMatch match)
        {
            AllMatches.Add(match);
        }

        public void Export(SpreadsheetDocument document, SheetData sheetData, WorksheetPart worksheetPart)
        {
            if (Groups.Count == 0)
                return;

            int rowsToCreate = RowsCounter.CalculateRowsToCreate(AllMatches.Count, Groups);
            RowsCreator.CreateRows(sheetData, rowsToCreate);

            int matchesInRow = 1;
            int matchRowNumber = 2;
            for (int i = 0; i < AllMatches.Count; i++)
            {
                var match = AllMatches[i];
                var matchPoints = Adapters.MatchAdapter.GetMatchInformationForExcelExport(match);
                var p1Cell = CellFinder.GetCell(worksheetPart.Worksheet, matchesInRow, matchRowNumber);
                p1Cell.CellValue = new CellValue(match.PlayerOne);
                p1Cell.DataType = new EnumValue<CellValues>(CellValues.String);
                var p1P = CellFinder.GetCell(worksheetPart.Worksheet, matchesInRow + 1, matchRowNumber);
                p1P.CellValue = new CellValue(matchPoints.Item1);
                p1P.DataType = new EnumValue<CellValues>(CellValues.Number);

                var p2Cell = CellFinder.GetCell(worksheetPart.Worksheet, matchesInRow, matchRowNumber + 1);
                p2Cell.CellValue = new CellValue(match.PlayerTwo);
                p2Cell.DataType = new EnumValue<CellValues>(CellValues.String);
                var p2P = CellFinder.GetCell(worksheetPart.Worksheet, matchesInRow + 1, matchRowNumber + 1);
                p2P.CellValue = new CellValue(matchPoints.Item2);
                p2P.DataType = new EnumValue<CellValues>(CellValues.Number);

                if (matchesInRow < 10)
                {
                    matchesInRow += 3;
                }
                else
                {
                    matchesInRow = 1;
                    matchRowNumber += 3;
                }
            }

            matchRowNumber += 3;
            foreach (var group in Groups)
            {
                int columnIndex = 1;
                var gCell = CellFinder.GetCell(worksheetPart.Worksheet, columnIndex, matchRowNumber);
                string gName = $"Group {group.Key}";
                gCell.CellValue = new CellValue(gName);
                gCell.DataType = new EnumValue<CellValues>(CellValues.String);

                foreach (var player in group.Value)
                {
                    matchRowNumber++;
                    var pCell = CellFinder.GetCell(worksheetPart.Worksheet, columnIndex, matchRowNumber);
                    pCell.CellValue = new CellValue(player.PlayerName);
                    pCell.DataType = new EnumValue<CellValues>(CellValues.String);

                    var dCell = CellFinder.GetCell(worksheetPart.Worksheet, columnIndex + 1, matchRowNumber);
                    dCell.CellValue = new CellValue(player.GoalDifference);
                    dCell.DataType = new EnumValue<CellValues>(CellValues.Number);

                    var pointsCell = CellFinder.GetCell(worksheetPart.Worksheet, columnIndex + 2, matchRowNumber);
                    pointsCell.CellValue = new CellValue(player.Points);
                    pointsCell.DataType = new EnumValue<CellValues>(CellValues.Number);
                }

                matchRowNumber += 2;
            }

            // Datei Speichern
            document.WorkbookPart.Workbook.Save();
        }
    }
}
