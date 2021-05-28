using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    public class Match
    {
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }

        public Match(Player playerOne, Player playerTwo)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
        }

        public Match()
        {

        }
    }
}
