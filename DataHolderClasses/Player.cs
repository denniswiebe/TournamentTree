using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace TournamentTree
{
    public class Player
    {
        public string PlayerName { get; set; }

        public int PlayerID { get; set; }

        public int Wins { get; set; }

        public int Ties { get; set; }

        public int Points { get => Wins * 3 + Ties; }

        public int GoalDifference { get; set; }

        public int Placement { get; set; }

        public bool IsWildCard { get; set; } = false;

        public Player(string name, int id)
        {
            PlayerName = name;
            PlayerID = id;
        }

        public Player(bool wildCard)
        {
            IsWildCard = wildCard;
            PlayerName = "Freewin";
            PlayerID = 0;
        }

        public override string ToString()
        {
            return PlayerID + ". " + PlayerName;
        }
    }
}
