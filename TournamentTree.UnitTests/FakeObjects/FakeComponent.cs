using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree.UnitTests.FakeObjects
{
    public class FakeComponent : Component
    {
        public List<Player> FreeWinsPresent = new List<Player>() { new Player(new Name("1"), new Identification(1)), new Player(new Name("2"), new Identification(2)), new Player(new Name("Wildcard"), new Identification(0)), new Player(new Name("Wildcard"), new Identification(0)) };
        public List<Player> NoFreeWinsPresent = new List<Player>() { new Player(new Name("1"), new Identification(1)), new Player(new Name("Wildcard"), new Identification(0)), new Player(new Name("2"), new Identification(2)),  new Player(new Name("Wildcard"), new Identification(0)) };
    }
}
