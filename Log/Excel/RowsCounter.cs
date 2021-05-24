using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    /// <summary>
    /// Diese Klasse zählt für die einzelnen Möglichkeiten des Excel-Exports die Zeilen, welche
    /// angelegt werden müssen, damit der Export funktioniert.
    /// </summary>
    public static class RowsCounter
    {
        /// <summary>
        /// Diese Methode zählt die Anzahl der anzulegenden Zeilen für den Turnierbaum.
        /// </summary>
        /// <param name="rounds">Runden des Turnierbaums</param>
        /// <returns></returns>
        public static int CalculateRowsToCreate(List<TournamentBracketLogRound> rounds)
        {
            int firstMatchRowIndex = 3;
            int maximumMatchesHeight = rounds[0].Matches.Count * 3 + rounds[0].Matches.Count - 1;
            return firstMatchRowIndex + maximumMatchesHeight;
        }

        /// <summary>
        /// Diese Methode zählt die Anzahl der anzulegenden Zeilen für die Gruppen.
        /// </summary>
        /// <param name="allMatchesCount">Anzahl aller Spiele</param>
        /// <param name="groups">Dictionary der Gruppen</param>
        /// <returns></returns>
        public static int CalculateRowsToCreate(int allMatchesCount, Dictionary<int, List<Player>> groups)
        {
            int firstMatchRowIndex = 3;
            var matchCount = Convert.ToDouble(allMatchesCount);
            var matchesHeight = matchCount / 4 * 2;
            var mH = Math.Round(matchesHeight, 0, MidpointRounding.AwayFromZero);
            int maximumMatchesHeight = Convert.ToInt32(mH) + Convert.ToInt32(mH) - 1;
            int groupsHeight = 1 + (groups.Count * groups[1].Count) + groups.Count - 1;
            return firstMatchRowIndex + maximumMatchesHeight + groupsHeight;
        }
    }
}
