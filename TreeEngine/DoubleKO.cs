using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// startet Doppel Ko System mit dem Winning Bracket
        /// </summary>
        public void StartWinnerTreeGenerator()
        {
            Console.WriteLine("Elimination starts!");
            while (Winners.Count > 1)
            {
                Console.WriteLine("Next Round!");
                List<Player> losers = new List<Player>();
                for (int i = 0; i < Winners.Count - 1; i += 2)
                {
                    if (Winners[i].PlayerID == 0 || Winners[i + 1].PlayerID == 0) // prüfen ob im Match eine Wildcard vorhanden ist
                    {
                        if (Winners[i].PlayerID == 0)
                        {
                            losers.Add(Winners[i]);
                        }
                        else
                        {
                            losers.Add(Winners[i + 1]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Who is the Winner?");
                        Console.WriteLine(Winners[i].ToString() + " Or " + Winners[i + 1].ToString());
                        bool correctPlayerInput = false;
                        while (!correctPlayerInput)
                        {
                            string whoWonInput = Console.ReadLine();
                            if (whoWonInput == Winners[i].PlayerName || whoWonInput == Winners[i].PlayerID.ToString())
                            {
                                Console.WriteLine("You Choose: " + Winners[i].ToString());
                                losers.Add(Winners[i + 1]);
                                correctPlayerInput = true;
                            }
                            else if (whoWonInput == Winners[i + 1].PlayerName || whoWonInput == Winners[i + 1].PlayerID.ToString())
                            {
                                Console.WriteLine("You Choose: " + Winners[i + 1].ToString());
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
                }

                // Sondersitation, damit nicht 3 Spieler im Loser übrig bleiben
                if (Winners.Count == 2)
                {
                    Console.Clear();
                    Console.WriteLine(CreateLosingTree());
                    PlayLoserBracket();
                }

                MoveLosingPlayers(losers);


                Console.Clear();
                if (Winners.Count > 1)
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

        /// <summary>
        /// Verschiebt die Spieler vom Winning Bracket in das Loser Bracket
        /// </summary>
        /// <param name="losers"></param>
        private void MoveLosingPlayers(List<Player> losers)
        {
            // Bei der ersten Loser Runde werden einfach alle Spieler in eine Liste getan
            if (FirstLosers)
            {
                foreach (Player loser in losers)
                {
                    Winners.Remove(loser);
                    Losers.Add(loser);
                }
                FirstLosers = false;
            }
            // Bei den nachfolgenden Runden müssen die Verlierer vom Winning Bracket gegen gezielte Gegner aus dem Loser Bracket spielen
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

        /// <summary>
        /// Spielt die Matches des Loser Brackets
        /// </summary>
        private void PlayLoserBracket()
        {
            List<Player> doubleLosers = new List<Player>();
            for (int i = 0; i < Losers.Count - 1; i += 2)
            {
                if (Losers[i].PlayerID == 0 && Losers[i + 1].PlayerID == 0) // prüfen ob beides WildCards sind
                {
                    doubleLosers.Add(Losers[i]);
                }
                else if ((Losers[i].PlayerID == 0 || Losers[i + 1].PlayerID == 0)) // prüfen ob im Match eine Wildcard vorhanden ist
                {
                    if (Losers[i].PlayerID == 0)
                    {
                        doubleLosers.Add(Losers[i]);
                    }
                    else
                    {
                        doubleLosers.Add(Losers[i + 1]);
                    }
                }
                else
                {

                    Console.WriteLine("Who is the Winner?");
                    Console.WriteLine(Losers[i].ToString() + " Or " + Losers[i + 1].ToString());
                    bool correctPlayerInput = false;
                    while (!correctPlayerInput)
                    {
                        string whoWonInput = Console.ReadLine();
                        if (whoWonInput == Losers[i].PlayerName || whoWonInput == Losers[i].PlayerID.ToString())
                        {
                            Console.WriteLine("You Choose: " + Losers[i].ToString());
                            doubleLosers.Add(Losers[i + 1]);
                            correctPlayerInput = true;
                        }
                        else if (whoWonInput == Losers[i + 1].PlayerName || whoWonInput == Losers[i + 1].PlayerID.ToString())
                        {
                            Console.WriteLine("You Choose: " + Losers[i + 1].ToString());
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
            }

            RemoveDoubleLosingPlayers(doubleLosers);

            // Für 16 Spieler muss an der Stelle nochmals Loser Bracket gespielt werden und dann erst wieder Winner
            if (Winners.Count == 4 && Losers.Count == 4)
            {
                Console.Clear();
                Console.WriteLine(CreateLosingTree());
                PlayLoserBracket();
            }
        }

        /// <summary>
        /// Nachdem ein Loser nochmal verliert scheidet er aus dem Turnier aus
        /// </summary>
        /// <param name="losers"></param>
        private void RemoveDoubleLosingPlayers(List<Player> losers)
        {
            foreach (Player loser in losers)
            {
                Losers.Remove(loser);
            }
        }


        /// <summary>
        /// Hier spielt der Gewinner vom Winning Bracket (winner) gegen den Gewinner vom Loserbracket (loser)
        /// Der Gewinner hat dabei zwei Versuche, wenn er beim ersten mal verliert hat er noch eine Chance da man nur bei zwei Niederlagen rausfliegt
        /// </summary>
        /// <param name="winner"></param>
        /// <param name="loser"></param>
        private void PlayFinale(Player winner, Player loser)
        {
            Console.WriteLine(ShowFinale());
            Console.WriteLine("Who is the Winner?");
            Console.WriteLine(winner.ToString() + " Or " + loser.ToString());
            bool correctPlayerInput = false;
            while (!correctPlayerInput)
            {
                string whoWonInput = Console.ReadLine();
                if (whoWonInput == winner.PlayerName || whoWonInput == winner.PlayerID.ToString())
                {
                    Console.WriteLine("You Choose: " + winner.ToString());
                    Console.WriteLine("Winner of the Tournament: " + winner.ToString());
                    _log.AddEntry("Winner: " + winner.ToString());
                    break;
                }
                else if (whoWonInput == loser.PlayerName || whoWonInput == loser.PlayerID.ToString())
                {
                    Console.WriteLine("You Choose: " + loser.ToString());
                    Console.WriteLine("Second Chance!");
                    Console.WriteLine("Who is the Winner?");
                    Console.WriteLine(winner.ToString() + " Or " + loser.ToString());
                    while (!correctPlayerInput)
                    {
                        string WhoWonTournament = Console.ReadLine();
                        if (WhoWonTournament == winner.PlayerName || WhoWonTournament == winner.PlayerID.ToString())
                        {
                            Console.WriteLine("You Choose: " + winner.ToString());
                            Console.WriteLine("Winner of the Tournament: " + winner.ToString());
                            _log.AddEntry("Winner: " + winner.ToString());
                            correctPlayerInput = true;
                        }
                        else if (WhoWonTournament == loser.PlayerName || WhoWonTournament == loser.PlayerID.ToString())
                        {
                            Console.WriteLine("You Choose: " + loser.ToString());
                            Console.WriteLine("Winner of the Tournament: " + loser.ToString());
                            _log.AddEntry("Winner: " + loser.ToString());
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

        /// <summary>
        /// Zeigt die beiden Letzten Spieler im Finale
        /// </summary>
        /// <returns></returns>
        private string ShowFinale()
        {
            string showFinals = "Finale\n";
            showFinals += "------------------------------------------------------\n\n";

            showFinals += Winners[0].ToString() + "\n";
            showFinals += "VERSUS\n";
            showFinals += Losers[0].ToString() + "\n";

            showFinals += "------------------------------------------------------\n\n";

            _log.AddEntry(showFinals);
            return showFinals;
        }

        /// <summary>
        /// Baut das Winning Bracket auf und zeigt es an
        /// </summary>
        /// <returns></returns>
        private string CreateWinningTree()
        {
            string showTree = "Winning Bracket\n";
            showTree += "------------------------------------------------------\n\n";

            if (FirstTree)
            {
                while (!NoFreeWinsAgainstEachOther())
                {
                    ShufflePlayers(Winners); // zum testen nicht shufflen
                }
            }


            for (int i = 0; i < Winners.Count; i++)
            {
                showTree += Winners[i].ToString() + "\n";
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

        /// <summary>
        /// Baut das LoserBracket auf und zeigt es an
        /// </summary>
        /// <returns></returns>
        private string CreateLosingTree()
        {
            string showTree = "Losing Bracket\n";
            showTree += "------------------------------------------------------\n\n";

            for (int i = 0; i < Losers.Count; i++)
            {
                showTree += Losers[i].ToString() + "\n";
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

        public bool NoFreeWinsAgainstEachOther()
        {
            bool check = true;
            for (int i = 0; i < Winners.Count() - 1; i++)
            {
                if (Winners[i].PlayerID == Winners[i + 1].PlayerID)
                {
                    check = false;
                }
            }
            return check;
        }

    }
}
