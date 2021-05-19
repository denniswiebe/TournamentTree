using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    public class TournamentBracketLogRound
    {
        public int Round;
        public List<TournamentBracketLogRoundMatch> Matches = new List<TournamentBracketLogRoundMatch>();

        public TournamentBracketLogRound(int round)
        {
            Round = round;
        }

        public void AddMatch(string playerOne, string playerTwo, bool firstPlayerWin)
        {
            var match = new TournamentBracketLogRoundMatch(playerOne, playerTwo, firstPlayerWin);
            Matches.Add(match);
        }
    }
}
