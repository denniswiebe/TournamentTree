using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentTree
{
    /// <summary>
    /// Holds necessary Methods for the components
    /// </summary>
    public class Component
    {

        public void ShuffleMatches(IList<Match> matches)
        {
            Random rand = new Random();
            for (int i = 0; i < matches.Count; i++)
            {
                var tempPlayer = matches[i]; // keep a Match in Mind to swap it with another
                var randomNumber = rand.Next(0, matches.Count);
                matches[i] = matches[randomNumber];
                matches[randomNumber] = tempPlayer;
            }
        }

        public void ShufflePlayers(IList<Player> playerList)
        {
            Random rand = new Random();
            for (int i = 0; i < playerList.Count; i++)
            {
                var tempPlayer = playerList[i]; // keep a Player in Mind to swap it with another
                var randomNumber = rand.Next(0, playerList.Count);
                playerList[i] = playerList[randomNumber];
                playerList[randomNumber] = tempPlayer;
            }
        }

        public bool NoFreeWinsAgainstEachOther(IList<Player> playerList)
        {
            bool check = true;
            for (int i = 0; i < playerList.Count() - 1; i++)
            {
                if (playerList[i].PlayerID.ToString() == playerList[i + 1].PlayerID.ToString())
                {
                    check = false;
                }
            }
            return check;
        }

        public void CreateLogOfTournament(ILog log, bool doubleKO = false)
        {
            Console.WriteLine("\nDo you want a Log of the Tournament? Y/N");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                log.CreateLog();
            }

            Console.WriteLine();
            Console.WriteLine("Do you want to create an Excel file of the tournament? Y/N");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                ExcelExporter.ExportToExcel(doubleKO);
            }
        }
    }
}
