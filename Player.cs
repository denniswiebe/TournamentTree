﻿using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace TournamentTree
{
    class Player
    {
        public string PlayerName { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public int Placement { get; set; }

        public Player(string name)
        {
            PlayerName = name;
        }
    }
}
