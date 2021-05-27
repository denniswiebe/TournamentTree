using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree.UnitTests.FakeObjects
{
    public class FakeTournamentGenerator : TournamentGenerator
    {
        public List<Player> FourPlayers = new List<Player>() { new Player("1", 1), new Player("2", 2), new Player("3", 3), new Player("4", 4) };
        public List<Player> ThreePlayers = new List<Player>() { new Player("1", 1), new Player("2", 2), new Player("3", 3) };
    }
}
