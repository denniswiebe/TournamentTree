using System;

namespace TournamentTree
{
    class Program
    {

        static void Main(string[] args)
        {
            var textArray = new[]
            {
             @"    _________                                                                     _       ______                                        _                   ",
             @"   |  _   _  |                                                                   / |_   .' ___  |                                      / |_                 ",
             @"   |_/ | | \_|.--.   __   _   _ .--.  _ .--.   ,--.   _ .--..--.  .---.  _ .--. `| |-' / .'   \_|  .---.  _ .--.  .---.  _ .--.  ,--. `| |-' .--.   _ .--.  ",
             @"       | |  / .'`\ \[  | | | [ `/'`\][ `.-. | `'_\ : [ `.-. .-. |/ /__\\[ `.-. | | |   | |   ____ / /__\\[ `.-. |/ /__\\[ `/'`\]`'_\ : | | / .'`\ \[ `/'`\] ",
             @"      _| |_ | \__. | | \_/ |, | |     | | | | // | |, | | | | | || \__., | | | | | |,  \ `.___]  || \__., | | | || \__., | |    // | |,| |,| \__. | | |     ",
             @"     |_____| '.__.'  '.__.'_/[___]   [___||__]\'-;__/[___||__||__]'.__.'[___||__]\__/   `._____.'  '.__.'[___||__]'.__.'[___]   \'-;__/\__/ '.__.' [___]    ",
            };

            Console.WindowWidth = 160;
            Console.WriteLine("\n");
            foreach (var item in textArray)
            {
                Console.WriteLine(item);
            }
            TournamentGenerator tournament = new TournamentGenerator();
            tournament.StartGenerate();
        }
    }
}