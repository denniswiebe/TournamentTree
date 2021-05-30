using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree.UnitTests.FakeObjects
{
    public class FakeTournamentGenerator : TournamentGenerator
    {
        public List<Player> FourPlayers = new List<Player>() { new Player(new Name("1"), new Identification(1)), new Player(new Name("2"), new Identification(2)), new Player(new Name("3"), new Identification(3)), new Player(new Name("4"), new Identification(4)) };
        public List<Player> ThreePlayers = new List<Player>() { new Player(new Name("1"), new Identification(1)), new Player(new Name("2"), new Identification(2)), new Player(new Name("3"), new Identification(3)) };
    }
}
