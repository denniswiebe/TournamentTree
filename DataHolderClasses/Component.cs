using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentTree
{
    /// <summary>
    /// Holds necessary Methods for the components
    /// </summary>
    class Component
    {
        public void ShuffleMatches(IList<Match> matches)
        {
            Random rand = new Random();
            for (int i = 0; i < matches.Count; i++)
            {
                var tempPlayer = matches[i]; // keep a Match in Mind to swap it with another
                var randomNumber = rand.Next(0, matches.Count);
                matches[i] = matches[randomNumber];
                matches[randomNumber] = tempPlayer;
            }
        }

        public void ShufflePlayers(IList<Player> playerList)
        {
            Random rand = new Random();
            for (int i = 0; i < playerList.Count; i++)
            {
                var tempPlayer = playerList[i]; // keep a Player in Mind to swap it with another
                var randomNumber = rand.Next(0, playerList.Count);
                playerList[i] = playerList[randomNumber];
                playerList[randomNumber] = tempPlayer;
            }
        }

        public bool NoFreeWinsAgainstEachOther(IList<Player> playerList)
        {
            bool check = true;
            for (int i = 0; i < playerList.Count() - 1; i++)
            {
                if (playerList[i].PlayerID == playerList[i + 1].PlayerID)
                {
                    check = false;
                }
            }
            return check;
        }
    }
}
