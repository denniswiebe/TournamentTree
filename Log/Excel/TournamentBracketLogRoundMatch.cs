using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    public class TournamentBracketLogRoundMatch
    {
        public string PlayerOne;
        public string PlayerTwo;
        public bool FirstPlayerWin;

        public TournamentBracketLogRoundMatch(string playerOne, string playerTwo, bool firstPlayerWin)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
            FirstPlayerWin = firstPlayerWin;
        }
    }
}
