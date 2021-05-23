using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TournamentTree
{
    public static class TournamentBracketLog
    {
        private static int startPosition = 3;
        private static int? nextStartposition = null;

        public static List<TournamentBracketLogRound> Rounds = new List<TournamentBracketLogRound>();

        /// <summary>
        /// Diese Methode erstellt aus den Turnierdaten eine Excel-Datei
        /// </summary>
        public static void GenerateBracketExcel(SpreadsheetDocument document, SheetData sheetData, WorksheetPart worksheetPart)
        {
            int rowsToCreate = CalculateRowsToCreate();
            CreateRows(sheetData, rowsToCreate);
            EvaluateRowData(worksheetPart);

            // Datei Speichern
            document.WorkbookPart.Workbook.Save();
        }

        /// <summary>
        /// Diese Methode wird benötigt, um die Anzahl der Reihen zu ermitteln,
        /// welche in der Excel-Datei angelegt werden müssen.
        /// Dies ist nötig, da die Excel-Datei sonst fehlerhaft gespeichert wird
        /// und somit Daten verliert, wenn beispielsweise in eine Zelle geschrieben wird,
        /// die es noch nicht gibt.
        /// </summary>
        /// <returns>Anzahl der anzulegenden Zeilen</returns>
        private static int CalculateRowsToCreate()
        {
            int firstMatchRowIndex = 3;
            int maximumMatchesHeight = Rounds[0].Matches.Count * 3 + Rounds[0].Matches.Count - 1;
            return firstMatchRowIndex + maximumMatchesHeight;
        }

        private static void CreateRows(SheetData sheetData, int rowsToCreate)
        {
            for (int i = 1; i <= rowsToCreate; i++)
            {
                var row = new Row { RowIndex = (uint)i };
                sheetData.AppendChild(row);
            }
        }

        private static void EvaluateRowData(WorksheetPart worksheetPart)
        {
            // Nun jede Runde der KO-Runde durchgehen
            for (int i = 0; i <= Rounds.Count; i++)
            {
                WriteRoundName(worksheetPart.Worksheet, i);
                WriteMatchOrWinner(worksheetPart, i);
                RemoveNextStartPositionValue();
            }
        }

        private static void WriteRoundName(Worksheet worksheet, int i)
        {
            string text = $"Round {i + 1}";
            if (i == Rounds.Count)
                text = "Winner";
            TournamentLog.WriteValueInCell(worksheet, CellValues.String, text, i + 1, 2);
        }

        private static void WriteMatchOrWinner(WorksheetPart worksheetPart, int i)
        {
            if (i < Rounds.Count)
                EvaluateMatchData(worksheetPart, i);
            else
                EvaluateWinner(worksheetPart, i);
        }

        private static void RemoveNextStartPositionValue()
        {
            if (nextStartposition.HasValue)
            {
                // Wenn die nextStartPosition-Variable nun gesetzt ist, dann wird startPosition
                // mit dem gesetzten Wert überschrieben, sodass die nächste Runde an dieser Stelle
                // weitermachen kann.
                startPosition = nextStartposition.Value;
                nextStartposition = null;
            }
        }

        private static void EvaluateMatchData(WorksheetPart worksheetPart, int i)
        {
            // Jedes Spiel jeder Runde durchgehen
            foreach (var match in Rounds[i].Matches)
            {
                // Daten des Spiels in die Zellen schreiben
                // Erster Spieler des Spiels
                TournamentLog.WriteValueInCell(worksheetPart.Worksheet, CellValues.String, match.PlayerOne, i + 1, startPosition);

                // VS
                var vsPosition = startPosition + Convert.ToInt32(Math.Pow(2, i));
                if (!nextStartposition.HasValue)
                    nextStartposition = vsPosition;
                TournamentLog.WriteValueInCell(worksheetPart.Worksheet, CellValues.String, "vs", i + 1, vsPosition);

                // Zweiter Spieler des Spiels
                TournamentLog.WriteValueInCell(worksheetPart.Worksheet, CellValues.String, match.PlayerTwo, i + 1, startPosition + 2 * Convert.ToInt32(Math.Pow(2, i)));

                // Nun wird die Startposition für das nächste Spiel berechnet
                startPosition = startPosition + Convert.ToInt32(Math.Pow(2, i + 2));
            }
        }

        private static void EvaluateWinner(WorksheetPart worksheetPart, int i)
        {
            // Gewinner eintragen
            var match = Rounds[i - 1].Matches[0];
            var winner = match.FirstPlayerWin ? match.PlayerOne : match.PlayerTwo;
            TournamentLog.WriteValueInCell(worksheetPart.Worksheet, CellValues.String, winner, i + 1, startPosition);
        }
    }
}
