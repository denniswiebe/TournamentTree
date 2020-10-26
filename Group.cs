using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    class Group
    {
        public List<Player> Players { get; set; }
        public char GroupChar { get; set; }


        public Group(List<Player> players)
        {
            Players = players;
        }
    }
}
