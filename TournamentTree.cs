using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentTree
{
    class TournamentTree
    {
        public IList<Player> Players { get; set; }

        public IList<Player> RemainingPlayers { get; set; } // Players are being kicked out in the future. Dont know if needed ?!

        public TournamentTree(List<Player> players)
        {
            Players = players;
            RemainingPlayers = Players;
            CreateTree();
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Elimination starts!");
        }

        public void CreateTree()
        {
            Console.WriteLine("------------------------------------------------------");
            Shuffle(Players);
            for (int i = 0; i < Players.Count; i++)
            {
                Console.WriteLine(Players[i].PlayerName);
                if (i % 2 == 0)
                {
                    Console.WriteLine("VERSUS");
                }
                else
                {
                    Console.WriteLine(""); // for a better Visualization who plays against each other
                }
            }    
        }

        public void Shuffle(IList<Player> playerList)
        {
            Random rand = new Random();
            for (int i = 0; i < playerList.Count; i++)
            {
                var tempPlayer = playerList[i]; // keep a Player in Mind to swap it with another
                var randomNumber = rand.Next(0, playerList.Count);
                playerList[i] = playerList[randomNumber];
                playerList[randomNumber] = tempPlayer;
;            }
        }

    }
}
