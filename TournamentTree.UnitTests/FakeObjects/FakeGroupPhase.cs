using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree.UnitTests.FakeObjects
{
    public class FakeGroupPhase : GroupPhase
    {
        public IList<Match> allMatchesGroup = new List<Match>() {
            new Match(new Player(new Name("1"), new Identification(1)), new Player(new Name("2"), new Identification(2))),
            new Match(new Player(new Name("3"), new Identification(3)), new Player(new Name("4"), new Identification(4))),
            new Match(new Player(new Name("5"), new Identification(5)), new Player(new Name("6"), new Identification(6))),
            new Match(new Player(new Name("7"), new Identification(7)), new Player(new Name("8"), new Identification(8))),
            new Match(new Player(new Name("9"), new Identification(9)), new Player(new Name("10"), new Identification(10))),
            new Match(new Player(new Name("11"), new Identification(11)), new Player(new Name("12"), new Identification(12))),
            new Match(new Player(new Name("13"), new Identification(13)), new Player(new Name("14"), new Identification(14))),
            new Match(new Player(new Name("15"), new Identification(15)), new Player(new Name("16"), new Identification(16)))
            };

        public IList<Match> allMatchesGroupOriginal = new List<Match>() {
            new Match(new Player(new Name("1"), new Identification(1)), new Player(new Name("2"), new Identification(2))),
            new Match(new Player(new Name("3"), new Identification(3)), new Player(new Name("4"), new Identification(4))),
            new Match(new Player(new Name("5"), new Identification(5)), new Player(new Name("6"), new Identification(6))),
            new Match(new Player(new Name("7"), new Identification(7)), new Player(new Name("8"), new Identification(8))),
            new Match(new Player(new Name("9"), new Identification(9)), new Player(new Name("10"), new Identification(10))),
            new Match(new Player(new Name("11"), new Identification(11)), new Player(new Name("12"), new Identification(12))),
            new Match(new Player(new Name("13"), new Identification(13)), new Player(new Name("14"), new Identification(14))),
            new Match(new Player(new Name("15"), new Identification(15)), new Player(new Name("16"), new Identification(16)))
            };

    }
}
