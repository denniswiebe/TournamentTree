using System;

namespace TournamentTree
{
    class Program
    {

        static void Main(string[] args)
        {
            TournamentGenerator tournament = new TournamentGenerator();
            tournament.StartGenerate();
        }
    }
}