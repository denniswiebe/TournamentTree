using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentTree
{
    class TournamentTree : Component
    {
        public IList<Player> Players { get; set; }

        public bool FirstTree { get; set; } = true;

        public TournamentLog _log = new TournamentLog(); 

        public TournamentTree(List<Player> players)
        {
            Players = players;
            Console.WriteLine(CreateTree());
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
                    Console.WriteLine(CreateTree());
                }
            }
            Console.Clear();
            Console.WriteLine("Winner of the Tournament: " + Players[0].PlayerName);
            _log.AddEntry("Winner: " + Players[0].PlayerName);

            //LogFile vom Turnier erstellen ?
            Console.WriteLine("Do you want a Log of the Tournament? Y/N");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                _log.CreateLog();
            }
        }

        private void EliminateLosingPlayers(List<Player> losers)
        {
            foreach (Player loser in losers)
            {
                Players.Remove(loser);
            }
        }

        private string CreateTree()
        {
            string showTree = "------------------------------------------------------\n\n";
            
            if (FirstTree)
            {
                ShufflePlayers(Players);
            }


            for (int i = 0; i < Players.Count; i++)
            {
                showTree += Players[i].PlayerName + "\n";
                if (i % 2 == 0)
                {
                    showTree += "VERSUS\n";
                }
                else
                {
                    showTree += "\n";
                }
            }
            showTree += "------------------------------------------------------\n";

            _log.AddEntry(showTree);
            return showTree;
        }
    }
}
