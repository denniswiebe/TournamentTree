using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    public static class TournamentDoubleKoLog
    {
        public static List<TournamentBracketLogRound> WinnerRounds = new List<TournamentBracketLogRound>();
        public static List<TournamentBracketLogRound> LoserRounds = new List<TournamentBracketLogRound>();
        public static List<TournamentBracketLogRoundMatch> FinalMatches = new List<TournamentBracketLogRoundMatch>();

        public static void GenerateBracketExcel(SpreadsheetDocument document, SheetData sheetData, WorksheetPart worksheetPart)
        {
            RowsCreator.CreateRows(sheetData, 100);

            // Variablen für die Startpositionen des ersten Spiels der jeweiligen Runde
            // nextStartPosition ist dafür da, sich die Position zu merken, wann das erste "VS"
            // der vorigen Runde gesetzt wird, da dort das erste Spiel der nächsten Runde beginnt,
            // damit die Excel-Datei am Ende wie ein richtiger Turnierbaum aussieht
            int startPosition = 3;
            int? nextStartposition = null;
            int? firstLoserStartPosition = null;

            // Nun jede Runde der KO-Runde durchgehen
            for (int i = 0; i <= WinnerRounds.Count; i++)
            {
                // Zelle ermittelt, in die der Rundenname geschrieben wird
                Cell cell = CellFinder.GetCell(worksheetPart.Worksheet, i + 1, 2);
                string text = $"Round {i + 1}";
                if (i == WinnerRounds.Count)
                    text = "Winner";

                // Rundenname in die Zelle schreiben
                cell.CellValue = new CellValue(text);
                cell.DataType = new EnumValue<CellValues>(CellValues.String);

                // Da die for-Schleife ein Mal öfter als die eigentliche Rundenanzahl läuft
                // gibt es hier eine Überprüfung, da es sonst zu einem Absturz kommen würde
                // Die for-Schleife läuft so oft, da am Ende ja noch der Gewinner in die Datei
                // geschrieben werden muss.
                if (i < WinnerRounds.Count)
                {
                    // Jedes Spiel jeder Runde durchgehen
                    foreach (var match in WinnerRounds[i].Matches)
                    {
                        // Daten des Spiels in die Zellen schreiben
                        // Erster Spieler des Spiels
                        var firstPlayerCell = CellFinder.GetCell(worksheetPart.Worksheet, i + 1, startPosition);
                        firstPlayerCell.CellValue = new CellValue(match.PlayerOne);
                        firstPlayerCell.DataType = new EnumValue<CellValues>(CellValues.String);

                        // VS
                        var vsPosition = startPosition + Convert.ToInt32(Math.Pow(2, i));
                        if (!nextStartposition.HasValue)
                            nextStartposition = vsPosition;
                        var vsCell = CellFinder.GetCell(worksheetPart.Worksheet, i + 1, vsPosition);
                        vsCell.CellValue = new CellValue("vs");
                        vsCell.DataType = new EnumValue<CellValues>(CellValues.String);

                        // Zweiter Spieler des Spiels
                        var secondPlayerCell = CellFinder.GetCell(worksheetPart.Worksheet, i + 1, startPosition + 2 * Convert.ToInt32(Math.Pow(2, i)));
                        secondPlayerCell.CellValue = new CellValue(match.PlayerTwo);
                        secondPlayerCell.DataType = new EnumValue<CellValues>(CellValues.String);

                        // Nun wird die Startposition für das nächste Spiel berechnet
                        startPosition = startPosition + Convert.ToInt32(Math.Pow(2, i + 2));
                    }

                    if (!firstLoserStartPosition.HasValue)
                    {
                        var position = startPosition + 2 * Convert.ToInt32(Math.Pow(2, i));
                        firstLoserStartPosition = position + 2;
                    }
                }
                else
                {
                    // Gewinner eintragen
                    var match = WinnerRounds[i - 1].Matches[0];
                    var winner = match.FirstPlayerWin ? match.PlayerOne : match.PlayerTwo;

                    var winnerCell = CellFinder.GetCell(worksheetPart.Worksheet, i + 1, startPosition);
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

            nextStartposition = null;
            startPosition = firstLoserStartPosition.Value;
            int? firstFinalPosition = null;
            for (int i = 0; i <= LoserRounds.Count; i++)
            {
                // Da die for-Schleife ein Mal öfter als die eigentliche Rundenanzahl läuft
                // gibt es hier eine Überprüfung, da es sonst zu einem Absturz kommen würde
                // Die for-Schleife läuft so oft, da am Ende ja noch der Gewinner in die Datei
                // geschrieben werden muss.
                if (i < LoserRounds.Count)
                {
                    // Jedes Spiel jeder Runde durchgehen
                    foreach (var match in LoserRounds[i].Matches)
                    {
                        // Daten des Spiels in die Zellen schreiben
                        // Erster Spieler des Spiels
                        var firstPlayerCell = CellFinder.GetCell(worksheetPart.Worksheet, i + 1, startPosition);
                        firstPlayerCell.CellValue = new CellValue(match.PlayerOne);
                        firstPlayerCell.DataType = new EnumValue<CellValues>(CellValues.String);

                        // VS
                        var vsPosition = startPosition + Convert.ToInt32(Math.Pow(2, i));
                        if (!nextStartposition.HasValue)
                            nextStartposition = vsPosition;
                        var vsCell = CellFinder.GetCell(worksheetPart.Worksheet, i + 1, vsPosition);
                        vsCell.CellValue = new CellValue("vs");
                        vsCell.DataType = new EnumValue<CellValues>(CellValues.String);

                        // Zweiter Spieler des Spiels
                        var secondPlayerCell = CellFinder.GetCell(worksheetPart.Worksheet, i + 1, startPosition + 2 * Convert.ToInt32(Math.Pow(2, i)));
                        secondPlayerCell.CellValue = new CellValue(match.PlayerTwo);
                        secondPlayerCell.DataType = new EnumValue<CellValues>(CellValues.String);

                        var position = 2 + startPosition + 2 * Convert.ToInt32(Math.Pow(2, i));
                        if (!firstFinalPosition.HasValue || position > firstFinalPosition.Value)
                            firstFinalPosition = position;

                        // Nun wird die Startposition für das nächste Spiel berechnet
                        startPosition = startPosition + Convert.ToInt32(Math.Pow(2, i + 2));
                    }
                }
                else
                {
                    // Gewinner eintragen
                    var match = LoserRounds[i - 1].Matches[0];
                    var winner = match.FirstPlayerWin ? match.PlayerOne : match.PlayerTwo;

                    var winnerCell = CellFinder.GetCell(worksheetPart.Worksheet, i + 1, startPosition);
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

            nextStartposition = null;
            startPosition = firstFinalPosition.Value;
            Cell finalCell = CellFinder.GetCell(worksheetPart.Worksheet, 1, startPosition);
            finalCell.CellValue = new CellValue("Finals");
            finalCell.DataType = new EnumValue<CellValues>(CellValues.String);
            startPosition++;
            foreach (var final in FinalMatches)
            {
                // Daten des Spiels in die Zellen schreiben
                // Erster Spieler des Spiels
                var firstPlayerCell = CellFinder.GetCell(worksheetPart.Worksheet, 1, startPosition);
                firstPlayerCell.CellValue = new CellValue(final.PlayerOne.ToString());
                firstPlayerCell.DataType = new EnumValue<CellValues>(CellValues.String);

                // VS
                var vsPosition = startPosition + 1;
                if (!nextStartposition.HasValue)
                    nextStartposition = vsPosition;
                var vsCell = CellFinder.GetCell(worksheetPart.Worksheet, 1, vsPosition);
                vsCell.CellValue = new CellValue("vs");
                vsCell.DataType = new EnumValue<CellValues>(CellValues.String);

                // Zweiter Spieler des Spiels
                var secondPlayerCell = CellFinder.GetCell(worksheetPart.Worksheet, 1, startPosition + 2);
                secondPlayerCell.CellValue = new CellValue(final.PlayerTwo.ToString());
                secondPlayerCell.DataType = new EnumValue<CellValues>(CellValues.String);

                var winnerCell = CellFinder.GetCell(worksheetPart.Worksheet, 2, vsPosition);
                var winner = final.FirstPlayerWin ? final.PlayerOne.ToString() : final.PlayerTwo.ToString();
                winnerCell.CellValue = new CellValue(winner);
                winnerCell.DataType = new EnumValue<CellValues>(CellValues.String);

                startPosition += 4;
            }

            // Datei Speichern
            document.WorkbookPart.Workbook.Save();
        }
    }
}
