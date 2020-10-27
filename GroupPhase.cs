using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TournamentTree
{
    class GroupPhase
    {
        public IList<Group> Groups { get; set; }

        public IList<Player> Players { get; set; }

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
            BuildingGroups(ValidateAmountOfGroups());
            PlacePlayersInGroups();
            ShowGroupsOnConsole();
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
            Console.WriteLine(amount + " Gruppen wurden erstellt.");
        }

        private void PlacePlayersInGroups()
        {
            Shuffle(Players);
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
                    Console.WriteLine("  " + player.PlayerName);
                }                
                Console.WriteLine();
            }
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
