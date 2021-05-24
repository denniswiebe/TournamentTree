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


        public Group(int id)
        {
            Players = new List<Player>();
            GroupId = id;
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public void SortPlayers() {
            //sort by Points
            Players = Players.OrderByDescending(player => player.Points).ToList();
            
            //if same points check if the player with also better goaldifference is in higher place
            for (int i = 0; i < Players.Count - 1; i++)
            {
                if(Players[i].Points == Players[i + 1].Points)
                {
                    if(Players[i].GoalDifference < Players[i + 1].GoalDifference)
                    {
                        var tempObject = Players[i];
                        Players[i] = Players[i + 1];
                        Players[i + 1] = tempObject;
                    }
                }
            }
        }
    }
}
