using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree.UnitTests
{
    [TestClass]
    public class SingleEliminationTest
    {
        [TestMethod]
        public void EliminateLosingPlayerTest()
        {
            Player playerA = new Player("A", 1);
            Player playerB = new Player("B", 2);
            Player playerC = new Player("C", 3);
            Player playerD = new Player("D", 4);
            List<Player> allPlayers = new List<Player>()
            {
                playerA,
                playerB,
                playerC,
                playerD
            };

            SingleElimination singleElimination = new SingleElimination(allPlayers);
            List<Player> loserPlayers = new List<Player>()
            {
                playerA,
                playerC
            };

            singleElimination.EliminateLosingPlayers(loserPlayers);



            Assert.IsTrue(singleElimination.Players.Count == 2);
            Assert.IsTrue(singleElimination.Players.Contains(playerB));
            Assert.IsTrue(singleElimination.Players.Contains(playerD));
            Assert.IsTrue(!singleElimination.Players.Contains(playerA));
            Assert.IsTrue(!singleElimination.Players.Contains(playerC));
        }

        [TestMethod]
        public void CreateTreeTest()
        {
            Player playerA = new Player("A", 1);
            Player playerB = new Player("B", 2);
            Player playerC = new Player("C", 3);
            Player playerD = new Player("D", 4);
            List<Player> allPlayers = new List<Player>()
            {
                playerA,
                playerB,
                playerC,
                playerD
            };
            SingleElimination singleElimination = new SingleElimination(allPlayers);
            singleElimination.FirstTree = false;

            string showTree = "Bracket";
            showTree += "\n------------------------------------------------------\n\n";
            showTree += singleElimination.Players[0].ToString() + "\n";
            showTree += "VERSUS\n";
            showTree += singleElimination.Players[1].ToString() + "\n";
            showTree += "\n";
            showTree += singleElimination.Players[2].ToString() + "\n";
            showTree += "VERSUS\n";
            showTree += singleElimination.Players[3].ToString() + "\n";
            showTree += "\n";
            showTree += "------------------------------------------------------\n";

            Assert.AreEqual(showTree, singleElimination.CreateTree(allPlayers));
        }
    }
}
