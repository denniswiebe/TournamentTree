using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree.UnitTests
{

    [TestClass]
    public class DoubleEliminationTest
    {
        [TestMethod]
        public void MoveLosingPlayerTest()
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

            DoubleElimination doubleElimination = new DoubleElimination(allPlayers);
            List<Player> loserPlayers = new List<Player>()
            {
                playerA,
                playerC
            };

            doubleElimination.MoveLosingPlayers(loserPlayers);

            Assert.IsTrue(doubleElimination.Winners.Count == 2);
            Assert.IsTrue(doubleElimination.Losers.Count == 2);
            Assert.IsTrue(doubleElimination.Losers.Contains(playerA));
            Assert.IsTrue(doubleElimination.Losers.Contains(playerC));
            Assert.IsTrue(doubleElimination.Winners.Contains(playerB));
            Assert.IsTrue(doubleElimination.Winners.Contains(playerD));
            
        }
    }
}
