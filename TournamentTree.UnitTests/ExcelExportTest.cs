using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TournamentTree.UnitTests
{
    [TestClass]
    public class ExcelExportTest
    {
        private const string PLAYER_ONE = "PlayerOne";
        private const string PLAYER_TWO = "PlayerTwo";
        private const string PLAYER_THREE = "PlayerThree";
        private const string PLAYER_FOUR = "PlayerFour";
        private const string PLAYER_FIVE = "PlayerFive";
        private const string PLAYER_SIX = "PlayerSix";
        private const string PLAYER_SEVEN = "PlayerSeven";
        private const string PLAYER_EIGHT = "PlayerEight";

        [TestMethod]
        public void GroupExportTest()
        {
            // Alle Gruppenspiele hinzufügen
            TournamentGroupLog.AddMatch(PLAYER_THREE, PLAYER_FOUR, 1, 1);
            TournamentGroupLog.AddMatch(PLAYER_FIVE, PLAYER_FOUR, 1, 1);
            TournamentGroupLog.AddMatch(PLAYER_THREE, PLAYER_SEVEN, 1, 1);
            TournamentGroupLog.AddMatch(PLAYER_SIX, PLAYER_ONE, 1, 1);
            TournamentGroupLog.AddMatch(PLAYER_FIVE, PLAYER_SEVEN, 1, 1);
            TournamentGroupLog.AddMatch(PLAYER_FOUR, PLAYER_SEVEN, 1, 1);
            TournamentGroupLog.AddMatch(PLAYER_ONE, PLAYER_TWO, 1, 1);
            TournamentGroupLog.AddMatch(PLAYER_SIX, PLAYER_EIGHT, 1, 1);
            TournamentGroupLog.AddMatch(PLAYER_SIX, PLAYER_TWO, 1, 1);
            TournamentGroupLog.AddMatch(PLAYER_ONE, PLAYER_EIGHT, 1, 1);
            TournamentGroupLog.AddMatch(PLAYER_FIVE, PLAYER_THREE, 1, 1);
            TournamentGroupLog.AddMatch(PLAYER_EIGHT, PLAYER_TWO, 1, 1);

            // Gruppen erzeugen
            var groupA = new List<Player>();
            groupA.Add(new Player(PLAYER_FIVE, 5));
            groupA.Add(new Player(PLAYER_THREE, 3));
            groupA.Add(new Player(PLAYER_FOUR, 4));
            groupA.Add(new Player(PLAYER_SEVEN, 7));

            var groupB = new List<Player>();
            groupB.Add(new Player(PLAYER_SIX, 6));
            groupB.Add(new Player(PLAYER_ONE, 1));
            groupB.Add(new Player(PLAYER_EIGHT, 8));
            groupB.Add(new Player(PLAYER_TWO, 2));

            // Gruppen hinzufügen
            TournamentGroupLog.Groups.Add(1, groupA);
            TournamentGroupLog.Groups.Add(2, groupB);

            // Dinge erzeugen, die wichtig für den Export sind
            var fileName = "KnockOutStageGroupExportTest.xlsx";
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                Sheets sheets = document.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                // Groups
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                SheetData sheetdata = new SheetData();
                if (TournamentGroupLog.Groups.Count > 0)
                {
                    Worksheet worksheet = new Worksheet();
                    worksheet.AppendChild(sheetdata);
                    worksheetPart.Worksheet = worksheet;
                    Sheet sheet1 = new Sheet()
                    {
                        Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
                        SheetId = 1,
                        Name = "Groups"
                    };
                    sheets.Append(sheet1);
                }

                // Den eigentlichen Export durchführen
                TournamentGroupLog.GenerateGroupExcel(document, sheetdata, worksheetPart);

                // Daten, die aus der Datei benötigt werden, sodass die Tests durchgeführt werden können
                var playerCell = CellFinder.GetCell(worksheetPart.Worksheet, 10, 2);
                var firstValueCell = CellFinder.GetCell(worksheetPart.Worksheet, 2, 2);
                var secondValueCell = CellFinder.GetCell(worksheetPart.Worksheet, 2, 3);
                var firstGroupNameCell = CellFinder.GetCell(worksheetPart.Worksheet, 1, 14);
                var groupWinnerCell = CellFinder.GetCell(worksheetPart.Worksheet, 1, 15);
                var groupDifferenceCell = CellFinder.GetCell(worksheetPart.Worksheet, 2, 21);
                var groupPointsCell = CellFinder.GetCell(worksheetPart.Worksheet, 3, 24);

                int firstValue, secondValue;
                firstValueCell.CellValue.TryGetInt(out firstValue);
                secondValueCell.CellValue.TryGetInt(out secondValue);

                // Tests, um zu überprüfen, ob der Export funktioniert hat.
                Assert.IsTrue(playerCell != null && playerCell.DataType == CellValues.String);
                Assert.IsTrue(firstValueCell != null && firstValueCell.DataType == CellValues.Number);
                Assert.AreEqual(firstValue, 1);
                Assert.AreEqual(secondValue, 1);
                Assert.AreEqual(firstGroupNameCell.CellValue.Text, "Group 1");
                Assert.IsTrue(groupWinnerCell != null && groupWinnerCell.DataType == CellValues.String);
                Assert.IsTrue(groupDifferenceCell != null && groupDifferenceCell.DataType == CellValues.Number);
                Assert.IsTrue(groupPointsCell != null && groupPointsCell.DataType == CellValues.Number);
            }
        }

        [TestMethod]
        public void KoExportTest()
        {
            // Runden für das KO-System erzeugen
            var tournamentBracketLogRoundOne = new TournamentBracketLogRound(1);
            tournamentBracketLogRoundOne.AddMatch(PLAYER_FIVE, PLAYER_THREE, true);
            tournamentBracketLogRoundOne.AddMatch(PLAYER_SIX, PLAYER_ONE, true);

            var tournamentBracketLogRoundTwo = new TournamentBracketLogRound(2);
            tournamentBracketLogRoundTwo.AddMatch(PLAYER_FIVE, PLAYER_SIX, false);

            // Runden hinzufügen
            TournamentBracketLog.Rounds.Add(tournamentBracketLogRoundOne);
            TournamentBracketLog.Rounds.Add(tournamentBracketLogRoundTwo);

            // Dinge erzeugen, die wichtig für den Export sind
            var fileName = "KnockOutStageGroupExportTest.xlsx";
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                Sheets sheets = document.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                Worksheet worksheet = new Worksheet();
                SheetData sheetdata = new SheetData();
                worksheet.AppendChild(sheetdata);
                worksheetPart.Worksheet = worksheet;
                Sheet sheet2 = new Sheet()
                {
                    Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 2,
                    Name = "KO"
                };
                sheets.Append(sheet2);

                // Den eigentlichen Export durchführen
                TournamentBracketLog.GenerateBracketExcel(document, sheetdata, worksheetPart);

                // Zellen aus dem Dokument laden, welche angeschaut werden müssen
                var firstUsedCell = CellFinder.GetCell(worksheetPart.Worksheet, 1, 2);
                var firstRoundStartCell = CellFinder.GetCell(worksheetPart.Worksheet, 1, 3);
                var secondRoundStartCell = CellFinder.GetCell(worksheetPart.Worksheet, 2, 4);
                var thirdRoundStartCell = CellFinder.GetCell(worksheetPart.Worksheet, 3, 6);

                // Tests durchführen
                Assert.IsTrue(firstUsedCell != null && firstUsedCell.DataType == CellValues.String);
                Assert.AreEqual(firstUsedCell.CellValue.Text, "Round 1");
                Assert.IsTrue(firstRoundStartCell != null && firstRoundStartCell.DataType == CellValues.String);
                Assert.IsTrue(secondRoundStartCell != null && secondRoundStartCell.DataType == CellValues.String);
                Assert.IsTrue(thirdRoundStartCell != null && thirdRoundStartCell.DataType == CellValues.String);
            }
        }
    }
}
