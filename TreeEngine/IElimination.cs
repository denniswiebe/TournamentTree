using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    interface IElimination
    {

        void StartElimination();

        void EliminateLosingPlayers(List<Player> losers);

        string CreateTree(IList<Player> players, string winnerOrLoserBracket = "Bracket");
    }
}
