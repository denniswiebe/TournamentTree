using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree.UnitTests.FakeObjects
{
    public class FakeSingleElimination : SingleElimination
    {
        public IList<Player> EightPlayers = new List<Player>() { new Player("1", 1), new Player("2", 2), new Player("3", 3), new Player("4", 4), new Player("5", 4), new Player("6", 4), new Player("7", 4), new Player("8", 4) };
        public IList<Player> OriginalPlayerOrder = new List<Player>() { new Player("1", 1), new Player("2", 2), new Player("3", 3), new Player("4", 4), new Player("5", 4), new Player("6", 4), new Player("7", 4), new Player("8", 4) };        
    }
}
