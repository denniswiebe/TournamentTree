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
            FakeObjects.FakeSingleElimination singleElimination = new FakeObjects.FakeSingleElimination(new TournamentLog());
            singleElimination.EliminateLosingPlayers(singleElimination.losers);

            Assert.IsTrue(singleElimination.Players.Count == 2);
            Assert.IsTrue(singleElimination.Players.Contains(singleElimination.playerB));
            Assert.IsTrue(singleElimination.Players.Contains(singleElimination.playerD));
            Assert.IsTrue(!singleElimination.Players.Contains(singleElimination.playerA));
            Assert.IsTrue(!singleElimination.Players.Contains(singleElimination.playerC));
        }

        [TestMethod]
        public void CreateTreeTest()
        {
            FakeObjects.FakeSingleElimination singleElimination = new FakeObjects.FakeSingleElimination(new TournamentLog());
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

            Assert.AreEqual(showTree, singleElimination.CreateTree(singleElimination.Players));
        }
    }
}
