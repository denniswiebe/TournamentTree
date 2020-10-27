using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    class Group
    {
        public IList<Player> Players { get; set; }
        public int GroupId { get; set; }


        public Group(int id)
        {
            Players = new List<Player>();
            GroupId = id;
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }
    }
}
