using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    public class TournamentGroupLogMatch
    {
        public string PlayerOne { get; private set; }
        public string PlayerTwo { get; private set; }
        public int PlayerOnePoints { get; private set; }
        public int PlayerTwoPoints { get; private set; }

        private TournamentGroupLogMatch(string p1, string p2, int p1p, int p2p)
        {
            PlayerOne = p1;
            PlayerTwo = p2;
            PlayerOnePoints = p1p;
            PlayerTwoPoints = p2p;
        }

        public static TournamentGroupLogMatch Create(string p1, string p2, int p1p, int p2p)
        {
            return new TournamentGroupLogMatch(p1, p2, p1p, p2p);
        }
    }
}
