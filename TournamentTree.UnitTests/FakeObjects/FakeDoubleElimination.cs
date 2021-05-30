using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree.UnitTests.FakeObjects
{
    public class FakeDoubleElimination : DoubleElimination
    {
        public Player playerA;
        public Player playerB;
        public Player playerC;
        public Player playerD;
        public List<Player> losers;

        public FakeDoubleElimination()
        {
            playerA = new Player(new Name("A"), new Identification(1));
            playerB = new Player(new Name("B"), new Identification(2));
            playerC = new Player(new Name("C"), new Identification(3));
            playerD = new Player(new Name("D"), new Identification(4));
            Winners = new List<Player>
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

        }
    }
}
