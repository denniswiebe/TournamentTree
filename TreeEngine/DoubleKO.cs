using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    class DoubleKO : Component
    {
        public IList<Player> Winners { get; set; }

        public IList<Player> Losers { get; set; } = new List<Player>();

        public bool FirstTree { get; set; } = true;

        public bool FirstLosers { get; set; } = true;

        public TournamentLog _log = new TournamentLog();

        public DoubleKO(List<Player> players)
        {
            Winners = players;
            Console.WriteLine(CreateWinningTree());
            FirstTree = false;
        }

        public void StartWinnerTreeGenerator()
        {
            Console.WriteLine("Elimination starts!");
            while (Winners.Count > 1)
            {
                Console.WriteLine("Next Round!");
                List<Player> losers = new List<Player>();
                for (int i = 0; i < Winners.Count - 1; i += 2)
                {
                    Console.WriteLine("Who is the Winner?");
                    Console.WriteLine(Winners[i].PlayerName + " Or " + Winners[i + 1].PlayerName);
                    bool correctPlayerInput = false;
                    while (!correctPlayerInput)
                    {
                        string whoWonInput = Console.ReadLine();
                        if (whoWonInput == Winners[i].PlayerName)
                        {
                            Console.WriteLine("You Choose: " + Winners[i].PlayerName);
                            losers.Add(Winners[i + 1]);
                            correctPlayerInput = true;
                        }
                        else if (whoWonInput == Winners[i + 1].PlayerName)
                        {
                            Console.WriteLine("You Choose: " + Winners[i + 1].PlayerName);
                            losers.Add(Winners[i]);
                            correctPlayerInput = true;
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input! Try Again.");
                        }
                        Console.WriteLine();
                    }
                }

                MoveLosingPlayers(losers);

                if (Winners.Count != 1)
                {
                    Console.Clear();
                    Console.WriteLine(CreateWinningTree());
                }
            }
            Console.Clear();


            //Console.WriteLine("Winner of the Tournament: " + Winners[0].PlayerName);
            //_log.AddEntry("Winner: " + Players[0].PlayerName);

            ////LogFile vom Turnier erstellen ?
            //Console.WriteLine("Do you want a Log of the Tournament? Y/N");
            //if (Console.ReadKey().Key == ConsoleKey.Y)
            //{
            //    _log.CreateLog();
            //}
        }

        private void MoveLosingPlayers(List<Player> losers)
        {
            if (FirstLosers)
            {
                foreach (Player loser in losers)
                {
                    Losers.Add(loser);
                    Winners.Remove(loser);
                }
                FirstLosers = false;
            }
            else
            {
                int counterIndex = 0;
                foreach (Player loser in losers)
                {
                    Losers.Insert(counterIndex, loser);
                    Winners.Remove(loser);
                    counterIndex += 2;
                }
            }
            Console.Clear();
            Console.WriteLine(CreateLosingTree());
            PlayLoserBracket();
        }

        private void PlayLoserBracket()
        {
            List<Player> losers = new List<Player>();
            for (int i = 0; i < Losers.Count - 1; i += 2)
            {
                Console.WriteLine("Who is the Winner?");
                Console.WriteLine(Losers[i].PlayerName + " Or " + Losers[i + 1].PlayerName);
                bool correctPlayerInput = false;
                while (!correctPlayerInput)
                {
                    string whoWonInput = Console.ReadLine();
                    if (whoWonInput == Losers[i].PlayerName)
                    {
                        Console.WriteLine("You Choose: " + Losers[i].PlayerName);
                        losers.Add(Losers[i + 1]);
                        correctPlayerInput = true;
                    }
                    else if (whoWonInput == Winners[i + 1].PlayerName)
                    {
                        Console.WriteLine("You Choose: " + Losers[i + 1].PlayerName);
                        losers.Add(Losers[i]);
                        correctPlayerInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Wrong Input! Try Again.");
                    }
                    Console.WriteLine();
                }
            }

            RemoveDoubleLosingPlayers(losers);
        }

        private void RemoveDoubleLosingPlayers(List<Player> losers)
        {
            foreach (Player loser in losers)
            {
                Losers.Remove(loser);
            }
        }

        private string CreateWinningTree()
        {
            string showTree = "Winning Bracket\n";
            showTree += "------------------------------------------------------\n\n";

            if (FirstTree)
            {
                //ShufflePlayers(Winners); zum testen nicht shufflen
            }


            for (int i = 0; i < Winners.Count; i++)
            {
                showTree += Winners[i].PlayerName + "\n";
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

        private string CreateLosingTree()
        {
            string showTree = "Losing Bracket\n";
            showTree += "------------------------------------------------------\n\n";

            for (int i = 0; i < Losers.Count; i++)
            {
                showTree += Losers[i].PlayerName + "\n";
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
            return showTree;
        }
    }
}
