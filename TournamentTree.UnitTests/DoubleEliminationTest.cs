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
            FakeObjects.FakeDoubleElimination doubleElimination = new FakeObjects.FakeDoubleElimination();
            doubleElimination.MoveLosingPlayers(doubleElimination.losers);

            Assert.IsTrue(doubleElimination.Winners.Count == 2);
            Assert.IsTrue(doubleElimination.Losers.Count == 2);
            Assert.IsTrue(doubleElimination.Losers.Contains(doubleElimination.playerA));
            Assert.IsTrue(doubleElimination.Losers.Contains(doubleElimination.playerC));
            Assert.IsTrue(doubleElimination.Winners.Contains(doubleElimination.playerB));
            Assert.IsTrue(doubleElimination.Winners.Contains(doubleElimination.playerD));
            
        }
    }
}
