using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace TournamentTree
{
    public class Player
    {
        public Name PlayerName { get; set; }

        public Identification PlayerID { get; set; }

        public int Wins { get; set; }

        public int Ties { get; set; }

        public int Points { get => Wins * 3 + Ties; }

        public int GoalDifference { get; set; }

        public bool IsWildCard { get; set; } = false;

        public Player(Name name, Identification id)
        {
            PlayerName = name;
            PlayerID = id;
        }

        public Player(bool wildCard)
        {
            IsWildCard = wildCard;
            PlayerName = new Name("Freewin");
            PlayerID = new Identification(0);
        }

        public override string ToString()
        {
            return PlayerID + ". " + PlayerName;
        }
    }
}
