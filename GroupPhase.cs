using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TournamentTree
{
    class GroupPhase
    {
        public List<Group> Groups { get; set; }

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
            Players = players;
            ValidateAmountOfGroups();
        }

        private int ValidateAmountOfGroups()
        {
            if(Players.Count < 12)
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

        }
    }
}
