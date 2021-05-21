using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    public static class TournamentGroupLog
    {
        public static List<TournamentGroupLogMatch> AllMatches { get; } = new List<TournamentGroupLogMatch>();
        public static Dictionary<int, List<Player>> Groups { get; } = new Dictionary<int, List<Player>>();

        public static void AddMatch(string p1, string p2, int p1p, int p2p)
        {
            AllMatches.Add(new TournamentGroupLogMatch(p1, p2, p1p, p2p));
        }

        public static void GenerateGroupExcel(SpreadsheetDocument document, SheetData sheetData, WorksheetPart worksheetPart)
        {
            if (Groups.Count == 0)
                return;

            // Hier wird die maximale Anzahl der Zeilen im Sheet berechnet
            // Dies ist nötig, da die Excel-Datei sonst fehlerhaft gespeichert wird
            // und somit Daten verliert, wenn beispielsweise in eine Zeile geschrieben wird,
            // die es noch nicht gibt
            int firstMatchRowIndex = 3;
            var matchCount = Convert.ToDouble(AllMatches.Count);
            var matchesHeight = matchCount / 4 * 2;
            var mH = Math.Round(matchesHeight, 0, MidpointRounding.AwayFromZero);
            int maximumMatchesHeight = Convert.ToInt32(mH) + Convert.ToInt32(mH) - 1;
            int groupsHeight = 1 + (Groups.Count * Groups[1].Count) + Groups.Count - 1;
            int rowsToCreate = firstMatchRowIndex + maximumMatchesHeight + groupsHeight;
            for (int i = 1; i <= rowsToCreate; i++)
            {
                var row = new Row { RowIndex = (uint)i };
                sheetData.AppendChild(row);
            }

            int matchesInRow = 1;
            int matchRowNumber = 2;
            for (int i = 0; i < AllMatches.Count; i++)
            {
                var match = AllMatches[i];
                var p1Cell = TournamentLog.GetCell(worksheetPart.Worksheet, TournamentLog.GetLetterByNumber(matchesInRow), matchRowNumber);
                p1Cell.CellValue = new CellValue(match.PlayerOne);
                p1Cell.DataType = new EnumValue<CellValues>(CellValues.String);
                var p1P = TournamentLog.GetCell(worksheetPart.Worksheet, TournamentLog.GetLetterByNumber(matchesInRow + 1), matchRowNumber);
                p1P.CellValue = new CellValue(match.PlayerOnePoints);
                p1P.DataType = new EnumValue<CellValues>(CellValues.Number);

                var p2Cell = TournamentLog.GetCell(worksheetPart.Worksheet, TournamentLog.GetLetterByNumber(matchesInRow), matchRowNumber + 1);
                p2Cell.CellValue = new CellValue(match.PlayerTwo);
                p2Cell.DataType = new EnumValue<CellValues>(CellValues.String);
                var p2P = TournamentLog.GetCell(worksheetPart.Worksheet, TournamentLog.GetLetterByNumber(matchesInRow + 1), matchRowNumber + 1);
                p2P.CellValue = new CellValue(match.PlayerTwoPoints);
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
                var gCell = TournamentLog.GetCell(worksheetPart.Worksheet, TournamentLog.GetLetterByNumber(columnIndex), matchRowNumber);
                string gName = $"Group {group.Key}";
                gCell.CellValue = new CellValue(gName);
                gCell.DataType = new EnumValue<CellValues>(CellValues.String);

                foreach (var player in group.Value)
                {
                    matchRowNumber++;
                    var pCell = TournamentLog.GetCell(worksheetPart.Worksheet, TournamentLog.GetLetterByNumber(columnIndex), matchRowNumber);
                    pCell.CellValue = new CellValue(player.PlayerName);
                    pCell.DataType = new EnumValue<CellValues>(CellValues.String);

                    var dCell = TournamentLog.GetCell(worksheetPart.Worksheet, TournamentLog.GetLetterByNumber(columnIndex + 1), matchRowNumber);
                    dCell.CellValue = new CellValue(player.GoalDifference);
                    dCell.DataType = new EnumValue<CellValues>(CellValues.Number);

                    var pointsCell = TournamentLog.GetCell(worksheetPart.Worksheet, TournamentLog.GetLetterByNumber(columnIndex + 2), matchRowNumber);
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
