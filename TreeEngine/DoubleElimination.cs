using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentTree
{
    public class DoubleElimination : Component, IElimination
    {
        public IList<Player> Winners { get; set; }

        public IList<Player> Losers { get; set; } = new List<Player>();

        public bool FirstTree { get; set; } = true;

        public bool FirstLosers { get; set; } = true;

        public ILog log;

        public DoubleElimination(List<Player> players, ILog log)
        {
            Winners = players;
            this.log = log;
        }

        public DoubleElimination()
        {

        }

        /// <summary>
        /// startet Doppel Ko System mit dem Winning Bracket
        /// </summary>
        public void StartElimination()
        {
            Console.WriteLine(CreateTree(Winners, "Winning Bracket"));
            Console.WriteLine("Elimination starts!");
            int round = 1;
            while (Winners.Count > 1)
            {
                Console.WriteLine("Next Round!");
                var winnerRound = new TournamentBracketLogRound(round);
                List<Player> losers = new List<Player>();
                for (int i = 0; i < Winners.Count - 1; i += 2)
                {
                    if (Winners[i].PlayerID.Id == 0 || Winners[i + 1].PlayerID.Id == 0) // prüfen ob im Match eine Wildcard vorhanden ist
                    {
                        if (Winners[i].PlayerID.Id == 0)
                        {
                            losers.Add(Winners[i]);
                            winnerRound.AddMatch(Winners[i].ToString(), Winners[i + 1].ToString(), false);
                        }
                        else
                        {
                            losers.Add(Winners[i + 1]);
                            winnerRound.AddMatch(Winners[i].ToString(), Winners[i + 1].ToString(), false);
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
                            if (whoWonInput == Winners[i].PlayerName.Title || whoWonInput == Winners[i].PlayerID.ToString())
                            {
                                Console.WriteLine("You Choose: " + Winners[i].ToString());
                                losers.Add(Winners[i + 1]);
                                correctPlayerInput = true;
                            }
                            else if (whoWonInput == Winners[i + 1].PlayerName.Title || whoWonInput == Winners[i + 1].PlayerID.ToString())
                            {
                                Console.WriteLine("You Choose: " + Winners[i + 1].ToString());
                                losers.Add(Winners[i]);
                                correctPlayerInput = true;
                            }
                            else
                            {
                                Console.WriteLine("Wrong Input! Try Again.");
                            }
                            if (correctPlayerInput)
                                winnerRound.AddMatch(Winners[i].ToString(), Winners[i + 1].ToString(), whoWonInput == Winners[i].PlayerName.Title || whoWonInput == Winners[i].PlayerID.ToString());
                            Console.WriteLine();
                        }
                    }
                }

                // Sondersitation, damit nicht 3 Spieler im Loser übrig bleiben
                if (Winners.Count == 2)
                {
                    Console.Clear();
                    Console.WriteLine(CreateTree(Losers, "Losing Bracket"));
                    PlayLoserBracket();
                }

                MoveLosingPlayers(losers);
                Console.Clear();
                Console.WriteLine(CreateTree(Losers, "Losing Bracket"));
                PlayLoserBracket();

                round++;
                TournamentDoubleKoLog.WinnerRounds.Add(winnerRound);

                Console.Clear();
                if (Winners.Count > 1)
                {
                    Console.WriteLine(CreateTree(Winners, "Winning Bracket"));
                }
            }

            Console.Clear();
            PlayFinale(Winners[0], Losers[0]);
            CreateLogOfTournament(log, true);
        }

        /// <summary>
        /// Verschiebt die Spieler vom Winning Bracket in das Loser Bracket
        /// </summary>
        /// <param name="losers"></param>
        public void MoveLosingPlayers(List<Player> losers)
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
        }

        /// <summary>
        /// Spielt die Matches des Loser Brackets
        /// </summary>
        private void PlayLoserBracket()
        {
            var loserRound = new TournamentBracketLogRound(1);
            List<Player> doubleLosers = new List<Player>();
            for (int i = 0; i < Losers.Count - 1; i += 2)
            {
                if (Losers[i].PlayerID.Id == 0 && Losers[i + 1].PlayerID.Id == 0) // prüfen ob beides WildCards sind
                {
                    doubleLosers.Add(Losers[i]);
                    loserRound.AddMatch(Losers[i].ToString(), Losers[i + 1].ToString(), false);
                }
                else if ((Losers[i].PlayerID.Id == 0 || Losers[i + 1].PlayerID.Id == 0)) // prüfen ob im Match eine Wildcard vorhanden ist
                {
                    if (Losers[i].PlayerID.Id == 0)
                    {
                        doubleLosers.Add(Losers[i]);
                        loserRound.AddMatch(Losers[i].ToString(), Losers[i + 1].ToString(), false);
                    }
                    else
                    {
                        doubleLosers.Add(Losers[i + 1]);
                        loserRound.AddMatch(Losers[i].ToString(), Losers[i + 1].ToString(), true);
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
                        if (whoWonInput == Losers[i].PlayerName.Title || whoWonInput == Losers[i].PlayerID.ToString())
                        {
                            Console.WriteLine("You Choose: " + Losers[i].ToString());
                            doubleLosers.Add(Losers[i + 1]);
                            correctPlayerInput = true;
                        }
                        else if (whoWonInput == Losers[i + 1].PlayerName.Title || whoWonInput == Losers[i + 1].PlayerID.ToString())
                        {
                            Console.WriteLine("You Choose: " + Losers[i + 1].ToString());
                            doubleLosers.Add(Losers[i]);
                            correctPlayerInput = true;
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input! Try Again.");
                        }
                        if (correctPlayerInput)
                            loserRound.AddMatch(Losers[i].ToString(), Losers[i + 1].ToString(), whoWonInput == Losers[i].PlayerName.Title || whoWonInput == Losers[i].PlayerID.ToString());
                        Console.WriteLine();
                    }
                }
            }

            EliminateLosingPlayers(doubleLosers);
            TournamentDoubleKoLog.LoserRounds.Add(loserRound);

            // Für 16 Spieler muss an der Stelle nochmals Loser Bracket gespielt werden und dann erst wieder Winner
            if (Winners.Count == 4 && Losers.Count == 4)
            {
                Console.Clear();
                Console.WriteLine(CreateTree(Losers, "Losing Bracket"));
                PlayLoserBracket();
            }
        }

        /// <summary>
        /// Nachdem ein Loser nochmal verliert scheidet er aus dem Turnier aus
        /// </summary>
        /// <param name="losers"></param>
        public void EliminateLosingPlayers(List<Player> losers)
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
                if (whoWonInput == winner.PlayerName.Title || whoWonInput == winner.PlayerID.ToString())
                {
                    Console.WriteLine("You Choose: " + winner.ToString());
                    Console.WriteLine("Winner of the Tournament: " + winner.ToString());
                    log.AddEntry("Winner: " + winner.ToString());
                    var match = new TournamentBracketLogRoundMatch(winner.ToString(), loser.ToString(), whoWonInput == winner.PlayerName.Title || whoWonInput == winner.PlayerID.ToString());
                    TournamentDoubleKoLog.FinalMatches.Add(match);
                    break;
                }
                else if (whoWonInput == loser.PlayerName.Title || whoWonInput == loser.PlayerID.ToString())
                {
                    var match = new TournamentBracketLogRoundMatch(winner.ToString(), loser.ToString(), whoWonInput == winner.PlayerName.Title || whoWonInput == winner.PlayerID.ToString());
                    TournamentDoubleKoLog.FinalMatches.Add(match);
                    Console.WriteLine("You Choose: " + loser.ToString());
                    Console.WriteLine("Second Chance!");
                    Console.WriteLine("Who is the Winner?");
                    Console.WriteLine(winner.ToString() + " Or " + loser.ToString());
                    while (!correctPlayerInput)
                    {
                        string WhoWonTournament = Console.ReadLine();
                        if (WhoWonTournament == winner.PlayerName.Title || WhoWonTournament == winner.PlayerID.ToString())
                        {
                            Console.WriteLine("You Choose: " + winner.ToString());
                            Console.WriteLine("Winner of the Tournament: " + winner.ToString());
                            log.AddEntry("Winner: " + winner.ToString());
                            correctPlayerInput = true;
                        }
                        else if (WhoWonTournament == loser.PlayerName.Title || WhoWonTournament == loser.PlayerID.ToString())
                        {
                            Console.WriteLine("You Choose: " + loser.ToString());
                            Console.WriteLine("Winner of the Tournament: " + loser.ToString());
                            log.AddEntry("Winner: " + loser.ToString());
                            correctPlayerInput = true;
                        }
                        if (correctPlayerInput)
                        {
                            var secondFinal = new TournamentBracketLogRoundMatch(winner.ToString(), loser.ToString(), WhoWonTournament == winner.PlayerName.Title || WhoWonTournament == winner.PlayerID.ToString());
                            TournamentDoubleKoLog.FinalMatches.Add(secondFinal);
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

            log.AddEntry(showFinals);
            return showFinals;
        }

        /// <summary>
        /// Baut das Winning Bracket auf und zeigt es an
        /// </summary>
        /// <returns></returns>
        public string CreateTree(IList<Player> players, string winnerOrLoserBracket = "Bracket")
        {
            string showTree = winnerOrLoserBracket;
            showTree += "\n------------------------------------------------------\n\n";

            if (FirstTree)
            {
                do
                {
                    ShufflePlayers(players);
                } while (!NoFreeWinsAgainstEachOther(players));
                FirstTree = false;
            }


            for (int i = 0; i < players.Count; i++)
            {
                showTree += players[i].ToString() + "\n";
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

            log.AddEntry(showTree);
            return showTree;
        }

    }
}
