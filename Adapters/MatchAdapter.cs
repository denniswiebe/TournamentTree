using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree.Adapters
{
    public static class MatchAdapter
    {
        public static Tuple<string, string> GetMatchInformationForExcelExport(TournamentGroupLogMatch match)
        {
            return new Tuple<string, string>(match.PlayerOnePoints.ToString(), match.PlayerTwoPoints.ToString());
        }
    }
}
