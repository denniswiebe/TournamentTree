using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TournamentTree
{
    class GroupPhase : Component
    {
        public IList<Group> Groups { get; set; }

        public IList<Player> Players { get; set; }

        public List<Player> RemainingPlayers { get; set; } = new List<Player>();

        public IList<Match> AllMatches { get; set; } = new List<Match>();

        enum AmountOfGroups : int
        {
            One = 1,
            Small = 2,
            Middle = 4,
            Big = 8,
            Giant = 16,
        }

        public GroupPhase(List<Player> players)
        {
            Groups = new List<Group>();
            Players = players;
        }

        /// <summary>
        /// Startmethode für das Spielen eines Turniers in Gruppen.
        /// </summary>
        public void StartGroupGenerator()
        {
            // Zuerst muss die Anzahl der Gruppen ermittelt werden.
            var amountOfGroups = ValidateAmountOfGroups();
            // Jetzt werden die Gruppen erzeugt.
            BuildingGroups(amountOfGroups);
            // Die Spieler werden nun in die Gruppen eingeteilt.
            PlacePlayersInGroups();
            // Die Gruppen in der Konsole anzeigen.
            ShowGroupsOnConsole();
            // Nun werden alle Spiele gespielt.
            PlayMatches();
            // Endergebnisse der Gruppenphase anzeigen.
            ShowGroupsOnConsole();
            // Ermitteln, wer die Gruppenphase überstanden hat.
            BestTwoPlayersRemain();
        }

        /// <summary>
        /// Diese Methode ermittelt die besten zwei Spieler der Gruppen.
        /// Da die Gruppen immer mindestens drei Spieler haben, ist dies kein Problem.
        /// </summary>
        private void BestTwoPlayersRemain()
        {
            foreach (Group group in Groups)
            {
                RemainingPlayers.Add(group.Players[0]);
                RemainingPlayers.Add(group.Players[1]);
            }
            ShowRemainingPlayers();
        }

        /// <summary>
        /// Diese Methode zeigt die übrigen Spieler an, die nun in einem Turnierbaum gegeneinader spielen.
        /// </summary>
        private void ShowRemainingPlayers()
        {
            Console.WriteLine("Remaining Players are:");
            foreach (Player player in RemainingPlayers)
            {
                Console.WriteLine(player.PlayerName);
            }
        }

        /// <summary>
        /// Diese Methode wird benötigt, um die Anzahl der anzulegenden Gruppen festzulegen.
        /// </summary>
        /// <returns>Anzahl der anzulegenden Gruppen.</returns>
        private int ValidateAmountOfGroups()
        {
            if (Players.Count < 6)
            {
                return (int)AmountOfGroups.One;
            }
            else if (Players.Count < 12)
            {
                return (int)AmountOfGroups.Small;
            }
            else if (Players.Count < 32)
            {
                return (int)AmountOfGroups.Middle;
            }
            else if (Players.Count < 64)
            {
                return (int)AmountOfGroups.Big;
            }
            else
            {
                return (int)AmountOfGroups.Giant;
            }
        }

        /// <summary>
        /// Diese Methode legt die Gruppen an.
        /// </summary>
        /// <param name="amount">Anzahl der anzulegenden Gruppen.</param>
        private void BuildingGroups(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Group group = new Group(i + 1);
                Groups.Add(group);
            }
            // Groups created
        }

        /// <summary>
        /// Diese Methode befüllt die Gruppen mit dem Spielern des Turniers.
        /// </summary>
        private void PlacePlayersInGroups()
        {
            ShufflePlayers(Players);
            int counter = 0;
            while (counter < Players.Count)
            {
                foreach (Group group in Groups)
                {
                    if (counter < Players.Count)
                    {
                        group.AddPlayer(Players[counter++]);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Diese Methode zeigt alle Gruppen in der Konsole an.
        /// </summary>
        private void ShowGroupsOnConsole()
        {
            Console.Clear();
            foreach (Group group in Groups)
            {
                group.SortPlayers();
                Console.WriteLine("Group " + group.GroupId);
                for (int i = 0; i < group.Players.Count; i++)
                {
                    Console.WriteLine("  " + group.Players[i].PlayerName + " P: " + group.Players[i].Points + " D: " + group.Players[i].GoalDifference);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Diese Methode kümmert sich um das Spielen der Gruppenspiele.
        /// </summary>
        private void PlayMatches()
        {
            Console.WriteLine("Press Enter to start playing.");
            Console.ReadLine();
            Console.Clear();
            CreateMatches();
            ShuffleMatches(AllMatches); // shuffle Matches to have more randomness
            foreach (Match match in AllMatches)
            {
                match.PlayMatch();
            }
        }

        /// <summary>
        /// Diese Methode erzeugt die Spiele in einer Gruppe, sodass jeder Spieler gegen die jeweils anderen aus der Gruppe spielt.
        /// </summary>
        private void CreateMatches()
        {
            foreach (Group group in Groups)
            {
                for (int i = 0; i < group.Players.Count; i++)
                {
                    for (int j = 0; j < group.Players.Count; j++)
                    {
                        if (j > i) // so that everyone plays only ones against each other
                        {
                            Match match = new Match(group.Players[i], group.Players[j]);
                            AllMatches.Add(match);
                        }
                    }
                }
            }
        }
    }
}
