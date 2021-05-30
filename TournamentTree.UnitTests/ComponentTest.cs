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

            FakeObjects.FakeGroupPhase groupPhase = new FakeObjects.FakeGroupPhase();
            groupPhase.ShuffleMatches(groupPhase.allMatchesGroup);

            bool isShuffled = false;
            for (int i = 0; i < groupPhase.allMatchesGroup.Count; i++)
            {
                if (groupPhase.allMatchesGroupOriginal[i].PlayerOne.PlayerID != groupPhase.allMatchesGroup[i].PlayerOne.PlayerID)
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
            FakeObjects.FakeSingleElimination tournamentTree = new FakeObjects.FakeSingleElimination(new TournamentLog());
            tournamentTree.ShufflePlayers(tournamentTree.EightPlayers);

            bool isShuffled = false;
            for (int i = 0; i < tournamentTree.EightPlayers.Count; i++)
            {
                if (tournamentTree.EightPlayers[i].PlayerID != tournamentTree.OriginalPlayerOrder[i].PlayerID)
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
            FakeObjects.FakeComponent component = new FakeObjects.FakeComponent();
            Assert.AreEqual(false, component.NoFreeWinsAgainstEachOther(component.FreeWinsPresent));
            Assert.AreEqual(true, component.NoFreeWinsAgainstEachOther(component.NoFreeWinsPresent));
        }
    }
}
