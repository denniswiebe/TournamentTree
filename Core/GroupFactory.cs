using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentTree.Core
{
    public class GroupFactory
    {
        public void AddPlayer(Group group, Player player)
        {
            group.Players.Add(player);
        }

        public void SortPlayers(Group group)
        {
            //sort by Points
            group.Players = group.Players.OrderByDescending(player => player.Points).ToList();

            //if same points check if the player with also better goaldifference is in higher place
            for (int i = 0; i < group.Players.Count - 1; i++)
            {
                if (group.Players[i].Points == group.Players[i + 1].Points)
                {
                    if (group.Players[i].GoalDifference < group.Players[i + 1].GoalDifference)
                    {
                        var tempObject = group.Players[i];
                        group.Players[i] = group.Players[i + 1];
                        group.Players[i + 1] = tempObject;
                    }
                }
            }
        }
    }
}
