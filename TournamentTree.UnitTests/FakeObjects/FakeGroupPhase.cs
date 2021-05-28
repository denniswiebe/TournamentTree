using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree.UnitTests.FakeObjects
{
    public class FakeGroupPhase : GroupPhase
    {
        public IList<Match> allMatchesGroup = new List<Match>() {
            new Match(new Player("1", 1), new Player("2", 2)),
            new Match(new Player("3", 3), new Player("4", 4)),
            new Match(new Player("5", 5), new Player("6", 6)),
            new Match(new Player("7", 7), new Player("8", 8)),
            new Match(new Player("9", 9), new Player("10", 10)),
            new Match(new Player("11", 11), new Player("12", 12)),
            new Match(new Player("13", 13), new Player("14", 14)),
            new Match(new Player("15", 15), new Player("16", 16)),
            };

        public IList<Match> allMatchesGroupOriginal = new List<Match>() {
            new Match(new Player("1", 1), new Player("2", 2)),
            new Match(new Player("3", 3), new Player("4", 4)),
            new Match(new Player("5", 5), new Player("6", 6)),
            new Match(new Player("7", 7), new Player("8", 8)),
            new Match(new Player("9", 9), new Player("10", 10)),
            new Match(new Player("11", 11), new Player("12", 12)),
            new Match(new Player("13", 13), new Player("14", 14)),
            new Match(new Player("15", 15), new Player("16", 16)),
            };

    }
}
