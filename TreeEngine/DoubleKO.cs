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

                if (Winners.Count == 2)
                {
                    Console.Clear();
                    Console.WriteLine(CreateLosingTree());
                    PlayLoserBracket();
                }

                MoveLosingPlayers(losers);


                Console.Clear();
                if(Winners.Count > 1)
                {
                    Console.WriteLine(CreateWinningTree());
                }
            }

            Console.Clear();
            PlayFinale(Winners[0], Losers[0]);
            //LogFile vom Turnier erstellen ?
            Console.WriteLine("\nDo you want a Log of the Tournament? Y/N");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                _log.CreateLog();
            }
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
                    Winners.Remove(loser);
                    Losers.Insert(counterIndex, loser);
                    counterIndex += 2;
                }
            }
            Console.Clear();
            Console.WriteLine(CreateLosingTree());

            PlayLoserBracket();
        }

        private void PlayLoserBracket()
        {
            List<Player> doubleLosers = new List<Player>();
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
                        doubleLosers.Add(Losers[i + 1]);
                        correctPlayerInput = true;
                    }
                    else if (whoWonInput == Losers[i + 1].PlayerName)
                    {
                        Console.WriteLine("You Choose: " + Losers[i + 1].PlayerName);
                        doubleLosers.Add(Losers[i]);
                        correctPlayerInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Wrong Input! Try Again.");
                    }
                    Console.WriteLine();
                }
            }            

            RemoveDoubleLosingPlayers(doubleLosers);
            //For 16 Players special
            if (Winners.Count == 4 && Losers.Count == 4)
            {
                Console.Clear();
                Console.WriteLine(CreateLosingTree());
                PlayLoserBracket();
            }
        }

        private void RemoveDoubleLosingPlayers(List<Player> losers)
        {
            foreach (Player loser in losers)
            {
                Losers.Remove(loser);
            }
        }

        private void PlayFinale(Player winner, Player loser)
        {
            Console.WriteLine(ShowFinale());
            Console.WriteLine("Who is the Winner?");
            Console.WriteLine(winner.PlayerName + " Or " + loser.PlayerName);
            bool correctPlayerInput = false;
            while (!correctPlayerInput)
            {
                string whoWonInput = Console.ReadLine();
                if (whoWonInput == winner.PlayerName)
                {
                    Console.WriteLine("You Choose: " + winner.PlayerName);
                    Console.WriteLine("Winner of the Tournament: " + winner.PlayerName);
                    _log.AddEntry("Winner: " + winner.PlayerName);
                    break;
                }
                else if (whoWonInput == loser.PlayerName)
                {
                    Console.WriteLine("You Choose: " + loser.PlayerName);
                    Console.WriteLine("Second Chance!");
                    Console.WriteLine("Who is the Winner?");
                    Console.WriteLine(winner.PlayerName + " Or " + loser.PlayerName);
                    while (!correctPlayerInput)
                    {
                        string WhoWonTournament = Console.ReadLine();
                        if (WhoWonTournament == winner.PlayerName)
                        {
                            Console.WriteLine("You Choose: " + winner.PlayerName);
                            Console.WriteLine("Winner of the Tournament: " + winner.PlayerName);
                            _log.AddEntry("Winner: " + winner.PlayerName);
                            correctPlayerInput = true;
                        }
                        else if (WhoWonTournament == loser.PlayerName)
                        {
                            Console.WriteLine("You Choose: " + loser.PlayerName);
                            Console.WriteLine("Winner of the Tournament: " + loser.PlayerName);
                            _log.AddEntry("Winner: " + loser.PlayerName);
                            correctPlayerInput = true;
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input! Try Again.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Wrong Input! Try Again.");
                }
                Console.WriteLine();
            }

        }

        private string ShowFinale()
        {
            string showFinals = "Finale\n";
            showFinals += "------------------------------------------------------\n\n";

            showFinals += Winners[0].PlayerName + "\n";
            showFinals += "VERSUS\n";
            showFinals += Losers[0].PlayerName + "\n";

            showFinals += "------------------------------------------------------\n\n";

            _log.AddEntry(showFinals);
            return showFinals;
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

            _log.AddEntry(showTree);
            return showTree;
        }
    }
}
