using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TournamentTree.UnitTests
{
    [TestClass]
    public class ComponentTest
    {
        [TestMethod]
        public void TestShuffleMatches()
        {
            Player playerA = new Player("A", 1);
            Player playerB = new Player("B", 2);
            Player playerC = new Player("C", 3);
            Player playerD = new Player("D", 4);
            Player playerE = new Player("E", 5);
            Player playerF = new Player("F", 6);
            Player playerG = new Player("G", 7);
            Player playerH = new Player("H", 8);
            Player playerI = new Player("I", 9);
            Player playerJ = new Player("J", 10);
            Player playerK = new Player("K", 11);
            Player playerL = new Player("L", 12);
            Player playerM = new Player("M", 13);
            Player playerN = new Player("N", 14);
            Player playerO = new Player("O", 15);
            Player playerP = new Player("P", 16);
            List<Player> allPlayers = new List<Player>
            {
                playerA,
                playerB,
                playerC,
                playerD,
                playerE,
                playerF,
                playerG,
                playerH,
                playerI,
                playerJ,
                playerK,
                playerL,
                playerM,
                playerN,
                playerO,
                playerP
            };

            Match matchOne = new Match(playerA, playerB);
            Match matchTwo = new Match(playerC, playerD);
            Match matchThree = new Match(playerE, playerF);
            Match matchFour = new Match(playerG, playerH);
            Match matchFive = new Match(playerI, playerI);
            Match matchSix = new Match(playerK, playerL);
            Match matchSeven = new Match(playerM, playerN);
            Match matchEight = new Match(playerO, playerP);
            IList<Match> allMatchesGroup = new List<Match>
            {
                matchOne,
                matchTwo,
                matchThree,
                matchFour,
                matchFive,
                matchSix,
                matchSeven,
                matchEight
            };
            IList<Match> allMatchesOriginalOrder = new List<Match>
            {
                matchOne,
                matchTwo,
                matchThree,
                matchFour,
                matchFive,
                matchSix,
                matchSeven,
                matchEight
            };

            GroupPhase groupPhase = new GroupPhase(allPlayers);
            groupPhase.AllMatches = allMatchesGroup;
            groupPhase.ShuffleMatches(groupPhase.AllMatches);

            bool isShuffled = false;
            for (int i = 0; i < allMatchesGroup.Count; i++)
            {
                if (allMatchesOriginalOrder[i].PlayerOne.PlayerID != groupPhase.AllMatches[i].PlayerOne.PlayerID)
                {
                    isShuffled = true;
                    break;
                }
            }

            Assert.IsTrue(isShuffled);
        }

        [TestMethod]
        public void TestShufflePlayers()
        {
            Player playerA = new Player("A", 1);
            Player playerB = new Player("B", 2);
            Player playerC = new Player("C", 3);
            Player playerD = new Player("D", 4);
            Player playerE = new Player("E", 5);
            Player playerF = new Player("F", 6);
            Player playerG = new Player("G", 7);
            Player playerH = new Player("H", 8);
            List<Player> allPlayers = new List<Player>
            {
                playerA,
                playerB,
                playerC,
                playerD,
                playerE,
                playerF,
                playerG,
                playerH,
            };
            IList<Player> allPlayersOriginalOrder = new List<Player>
            {
                playerA,
                playerB,
                playerC,
                playerD,
                playerE,
                playerF,
                playerG,
                playerH,
            };

            SingleElimination tournamentTree = new SingleElimination(allPlayers);
            tournamentTree.ShufflePlayers(tournamentTree.Players);

            bool isShuffled = false;
            for (int i = 0; i < allPlayers.Count; i++)
            {
                if (allPlayersOriginalOrder[i].PlayerID != tournamentTree.Players[i].PlayerID)
                {
                    isShuffled = true;
                    break;
                }
            }
            Assert.IsTrue(isShuffled);
        }

        [TestMethod]
        public void TestNoFreeWinsAgainsEachOther()
        {
            Player playerA = new Player("A", 1);
            Player playerB = new Player("B", 2);
            Player playerWildCardC = new Player("C", 0);
            Player playerWildCardD = new Player("D", 0);
            List<Player> allPlayersFalse = new List<Player>
            {
                playerA,
                playerB,
                playerWildCardC,
                playerWildCardD,
            };
            List<Player> allPlayersTrue = new List<Player>
            {
                playerA,
                playerWildCardC,
                playerB,
                playerWildCardD,
            };
            Component component = new Component();
            Assert.AreEqual(false, component.NoFreeWinsAgainstEachOther(allPlayersFalse));
            Assert.AreEqual(true, component.NoFreeWinsAgainstEachOther(allPlayersTrue));
        }
    }
}
