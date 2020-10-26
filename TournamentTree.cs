using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentTree
{
    class TournamentTree
    {
        public IList<Player> Players { get; set; }

        public bool FirstTree { get; set; } = true;

        public TournamentTree(List<Player> players)
        {
            Players = players;
            CreateTree();
            FirstTree = false;
        }

        public void StartTreeGenerator()
        {
            Console.WriteLine("Elimination starts!");
            while (Players.Count != 1)
            {
                Console.WriteLine("Next Round!");
                List<Player> losers = new List<Player>();
                for (int i = 0; i < Players.Count - 1; i += 2)
                {
                    Console.WriteLine("Who is the Winner?");
                    Console.WriteLine(Players[i].PlayerName + " Or " + Players[i + 1].PlayerName);
                    bool correctPlayerInput = false;
                    while (!correctPlayerInput)
                    {
                        string whoWonInput = Console.ReadLine();
                        if (whoWonInput == Players[i].PlayerName)
                        {
                            Console.WriteLine("You Choose: " + Players[i].PlayerName);
                            losers.Add(Players[i + 1]);
                            correctPlayerInput = true;
                        }
                        else if (whoWonInput == Players[i + 1].PlayerName)
                        {
                            Console.WriteLine("You Choose: " + Players[i + 1].PlayerName);
                            losers.Add(Players[i]);
                            correctPlayerInput = true;
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input! Try Again.");
                        }
                        Console.WriteLine();
                    }
                }

                EliminateLosingPlayers(losers);
                
                if (Players.Count != 1)
                {
                    Console.Clear();
                    CreateTree();
                }
            }
            Console.Clear();
            Console.WriteLine("Winner of the Tournament: " + Players[0].PlayerName);
        }

        private void EliminateLosingPlayers(List<Player> losers)
        {
            foreach (Player loser in losers)
            {
                Players.Remove(loser);
            }
        }

        private void CreateTree()
        {
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine();
            if (FirstTree)
            {
                Shuffle(Players);
            }
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
            Console.WriteLine("------------------------------------------------------");
        }

        private void Shuffle(IList<Player> playerList)
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

    }
}
