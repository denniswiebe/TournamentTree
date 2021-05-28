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
            playerA = new Player("A", 1);
            playerB = new Player("B", 2);
            playerC = new Player("C", 3);
            playerD = new Player("D", 4);
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
