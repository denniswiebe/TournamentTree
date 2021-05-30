using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace TournamentTree
{
    /// <summary>
    /// 
    /// </summary>
    public class Group
    {
        public IList<Player> Players { get; set; }
        public int GroupId { get; set; }

        public Group(int id) : this(id, new List<Player>()) { }

        public Group(int id, List<Player> players)
        {
            GroupId = id;
            Players = players;
        }

    }
}
