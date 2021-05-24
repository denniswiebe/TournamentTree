using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    interface ITournamentTree
    {

        void StartTreeGenerator();

        void EliminateLosingPlayers(List<Player> losers);

        string CreateTree();
    }
}
