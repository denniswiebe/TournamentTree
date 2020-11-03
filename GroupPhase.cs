using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TournamentTree
{
    class GroupPhase : Component
    {
        public IList<Group> Groups { get; set; }

        public IList<Player> Players { get; set; }

        public IList<Match> AllMatches { get; set; } = new List<Match>();

        enum AmountOfGroups : int
        {
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

        public void GenerateGroups()
        {            
            BuildingGroups(ValidateAmountOfGroups()); // Create Groups based on amount of Players
            PlacePlayersInGroups(); // place the players inside the groups
            ShowGroupsOnConsole(); // Show initalized Groups
            PlayMatches(); // play all matches
            ShowGroupsOnConsole(); //
        }

        private int ValidateAmountOfGroups()
        {
            if (Players.Count < 12)
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

        private void BuildingGroups(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Group group = new Group(i+1);
                Groups.Add(group);
            }
            // Groups created
        }

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
            // Players randomly placed inside the Groups.
        }

        private void ShowGroupsOnConsole()
        {            
            Console.Clear();
            foreach (Group group in Groups)
            {
                Console.WriteLine("Group " + group.GroupId);
                foreach (Player player in group.Players)
                {
                    Console.WriteLine("  " + player.PlayerName + " P: " + player.Points + " D: " + player.GoalDifference);
                }                
                Console.WriteLine();
            }
        }

        private void PlayMatches()
        {
            Console.WriteLine("Press anything to start playing.");
            Console.ReadLine();
            Console.Clear();
            CreateMatches();
            ShuffleMatches(AllMatches); // shuffle Matches to have more randomness
            foreach (Match m in AllMatches)
            {
                m.PlayMatch();
            }
        }

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
