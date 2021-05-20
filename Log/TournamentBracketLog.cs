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
        public static List<TournamentBracketLogRound> Rounds = new List<TournamentBracketLogRound>();

        /// <summary>
        /// Diese Methode erstellt aus den Turnierdaten eine Excel-Datei
        /// </summary>
        public static void GenerateBracketExcel(SpreadsheetDocument document, SheetData sheetData, WorksheetPart worksheetPart)
        {
            // Hier wird die maximale Anzahl der Zeilen im Sheet berechnet
            // Dies ist nötig, da die Excel-Datei sonst fehlerhaft gespeichert wird
            // und somit Daten verliert, wenn beispielsweise in eine Zeile geschrieben wird,
            // die es noch nicht gibt
            int firstMatchRowIndex = 3;
            int maximumMatchesHeight = Rounds[0].Matches.Count * 3 + Rounds[0].Matches.Count - 1;
            int rowsToCreate = firstMatchRowIndex + maximumMatchesHeight;
            for (int i = 1; i <= rowsToCreate; i++)
            {
                var row = new Row { RowIndex = (uint)i };
                sheetData.AppendChild(row);
            }

            // Variablen für die Startpositionen des ersten Spiels der jeweiligen Runde
            // nextStartPosition ist dafür da, sich die Position zu merken, wann das erste "VS"
            // der vorigen Runde gesetzt wird, da dort das erste Spiel der nächsten Runde beginnt,
            // damit die Excel-Datei am Ende wie ein richtiger Turnierbaum aussieht
            int startPosition = 3;
            int? nextStartposition = null;

            // Nun jede Runde der KO-Runde durchgehen
            for (int i = 0; i <= Rounds.Count; i++)
            {
                // Zelle ermittelt, in die der Rundenname geschrieben wird
                Cell cell = TournamentLog.GetCell(worksheetPart.Worksheet, TournamentLog.GetLetterByNumber(i + 1), 2);
                string text = $"Round {i + 1}";
                if (i == Rounds.Count)
                    text = "Winner";

                // Rundenname in die Zelle schreiben
                cell.CellValue = new CellValue(text);
                cell.DataType = new EnumValue<CellValues>(CellValues.String);

                // Da die for-Schleife ein Mal öfter als die eigentliche Rundenanzahl läuft
                // gibt es hier eine Überprüfung, da es sonst zu einem Absturz kommen würde
                // Die for-Schleife läuft so oft, da am Ende ja noch der Gewinner in die Datei
                // geschrieben werden muss.
                if (i < Rounds.Count)
                {
                    // Jedes Spiel jeder Runde durchgehen
                    foreach (var match in Rounds[i].Matches)
                    {
                        // Daten des Spiels in die Zellen schreiben
                        // Erster Spieler des Spiels
                        var firstPlayerCell = TournamentLog.GetCell(worksheetPart.Worksheet, TournamentLog.GetLetterByNumber(i + 1), startPosition);
                        firstPlayerCell.CellValue = new CellValue(match.PlayerOne);
                        firstPlayerCell.DataType = new EnumValue<CellValues>(CellValues.String);

                        // VS
                        var vsPosition = startPosition + Convert.ToInt32(Math.Pow(2, i));
                        if (!nextStartposition.HasValue)
                            nextStartposition = vsPosition;
                        var vsCell = TournamentLog.GetCell(worksheetPart.Worksheet, TournamentLog.GetLetterByNumber(i + 1), vsPosition);
                        vsCell.CellValue = new CellValue("vs");
                        vsCell.DataType = new EnumValue<CellValues>(CellValues.String);

                        // Zweiter Spieler des Spiels
                        var secondPlayerCell = TournamentLog.GetCell(worksheetPart.Worksheet, TournamentLog.GetLetterByNumber(i + 1), startPosition + 2 * Convert.ToInt32(Math.Pow(2, i)));
                        secondPlayerCell.CellValue = new CellValue(match.PlayerTwo);
                        secondPlayerCell.DataType = new EnumValue<CellValues>(CellValues.String);

                        // Nun wird die Startposition für das nächste Spiel berechnet
                        startPosition = startPosition + Convert.ToInt32(Math.Pow(2, i + 2));
                    }
                }
                else
                {
                    // Gewinner eintragen
                    var match = Rounds[i - 1].Matches[0];
                    var winner = match.FirstPlayerWin ? match.PlayerOne : match.PlayerTwo;

                    var winnerCell = TournamentLog.GetCell(worksheetPart.Worksheet, TournamentLog.GetLetterByNumber(i + 1), startPosition);
                    winnerCell.CellValue = new CellValue(winner);
                    winnerCell.DataType = new EnumValue<CellValues>(CellValues.String);
                }

                if (nextStartposition.HasValue)
                {
                    // Wenn die nextStartPosition-Variable nun gesetzt ist, dann wird startPosition
                    // mit dem gesetzten Wert überschrieben, sodass die nächste Runde an dieser Stelle
                    // weitermachen kann.
                    startPosition = nextStartposition.Value;
                    nextStartposition = null;
                }
            }

            // Datei Speichern
            document.WorkbookPart.Workbook.Save();
        }
    }
}
