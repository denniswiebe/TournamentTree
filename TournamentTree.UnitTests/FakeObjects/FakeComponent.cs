using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree.UnitTests.FakeObjects
{
    public class FakeComponent : Component
    {
        public List<Player> FreeWinsPresent = new List<Player>() { new Player("1", 1), new Player("2", 2), new Player("Wildcard", 0), new Player("Wildcard", 0) };
        public List<Player> NoFreeWinsPresent = new List<Player>() { new Player("1", 1), new Player("Wildcard", 0), new Player("2", 2),  new Player("Wildcard", 0) };
    }
}
