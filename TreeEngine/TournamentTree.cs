﻿using System;
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
            int round = 1;
            while (Players.Count != 1)
            {
                Console.WriteLine("Next Round!");
                var tournamentBracketLogRound = new TournamentBracketLogRound(round);
                List<Player> losers = new List<Player>();
                for (int i = 0; i < Players.Count - 1; i += 2)
                {
                    if (Players[i].PlayerID == 0 || Players[i + 1].PlayerID == 0) // prüfen ob im Match eine Wildcard vorhanden ist
                    {
                        if (Players[i].PlayerID == 0)
                        {
                            losers.Add(Players[i]);
                            tournamentBracketLogRound.AddMatch(Players[i].ToString(), Players[i + 1].ToString(), false);
                        }
                        else
                        {
                            losers.Add(Players[i + 1]);
                            tournamentBracketLogRound.AddMatch(Players[i].ToString(), Players[i + 1].ToString(), true);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Who is the Winner? Name or ID");
                        Console.WriteLine(Players[i].ToString() + " Or " + Players[i + 1].ToString());
                        bool correctPlayerInput = false;
                        while (!correctPlayerInput)
                        {
                            string whoWonInput = Console.ReadLine();
                            if (whoWonInput == Players[i].PlayerName || whoWonInput == Players[i].PlayerID.ToString())
                            {
                                Console.WriteLine("You Choose: " + Players[i].ToString());
                                losers.Add(Players[i + 1]);
                                correctPlayerInput = true;
                            }
                            else if (whoWonInput == Players[i + 1].PlayerName || whoWonInput == Players[i + 1].PlayerID.ToString())
                            {
                                Console.WriteLine("You Choose: " + Players[i + 1].ToString());
                                losers.Add(Players[i]);
                                correctPlayerInput = true;
                            }
                            else
                            {
                                Console.WriteLine("Wrong Input! Try Again.");
                            }
                            if (correctPlayerInput)
                                tournamentBracketLogRound.AddMatch(Players[i].ToString(), Players[i + 1].ToString(), whoWonInput == Players[i].ToString());
                            Console.WriteLine();
                        }
                    }
                }

                EliminateLosingPlayers(losers);
                round++;
                TournamentBracketLog.Rounds.Add(tournamentBracketLogRound);

                if (Players.Count != 1)
                {
                    Console.Clear();
                    Console.WriteLine(CreateTree());
                }
            }
            Console.Clear();
            Console.WriteLine("Winner of the Tournament: " + Players[0].ToString());
            _log.AddEntry("Winner: " + Players[0].ToString());

            //LogFile vom Turnier erstellen ?
            Console.WriteLine("Do you want a Log of the Tournament? Y/N");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                _log.CreateLog();
            }

            Console.WriteLine();
            Console.WriteLine("Do you want to create an Excel file of the knockout-stage? Y/N");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                TournamentBracketLog.GenerateBracketExcel();
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
                while (!NoFreeWinsAgainstEachOther())
                {
                    ShufflePlayers(Players);
                }
            }


            for (int i = 0; i < Players.Count; i++)
            {
                showTree += Players[i].ToString() + "\n";
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
            for (int i = 0; i < Players.Count() - 1; i++)
            {
                if (Players[i].PlayerID == Players[i + 1].PlayerID)
                {
                    check = false;
                }
            }
            return check;
        }
    }
}
