using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree.UnitTests.FakeObjects
{
    public class FakeSingleElimination : SingleElimination
    {
        public IList<Player> EightPlayers = new List<Player>() { new Player(new Name("1"), new Identification(1)), new Player(new Name("2"), new Identification(2)), new Player(new Name("3"), new Identification(3)), new Player(new Name("4"), new Identification(4)), new Player(new Name("5"), new Identification(4)), new Player(new Name("6"), new Identification(4)), new Player(new Name("7"), new Identification(4)), new Player(new Name("8"), new Identification(4)) };
        public IList<Player> OriginalPlayerOrder = new List<Player>() { new Player(new Name("1"), new Identification(1)), new Player(new Name("2"), new Identification(2)), new Player(new Name("3"), new Identification(3)), new Player(new Name("4"), new Identification(4)), new Player(new Name("5"), new Identification(4)), new Player(new Name("6"), new Identification(4)), new Player(new Name("7"), new Identification(4)), new Player(new Name("8"), new Identification(4)) };

        public List<Player> losers;
        public Player playerA;
        public Player playerB;
        public Player playerC;
        public Player playerD;

        public FakeSingleElimination(ILog log)
        {
            playerA = new Player(new Name("A"), new Identification(1));
            playerB = new Player(new Name("B"), new Identification(2));
            playerC = new Player(new Name("C"), new Identification(3));
            playerD = new Player(new Name("D"), new Identification(4));
            Players = new List<Player>
            {
                playerA,
                playerB,
                playerC,
                playerD
            };
            losers = new List<Player>
            {
                playerA,
                playerC
            };
            this.log = log;
        }
    }
}
