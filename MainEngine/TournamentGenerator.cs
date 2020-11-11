﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentTree
{

    class TournamentGenerator
    {
        #region Private Members

        /// <summary>
        /// Minimale Anzahl an Spielern, die angelegt werden müssen.
        /// </summary>
        private const int MINIMUM_PLAYERS_COUNT = 2;

        /// <summary>
        /// Minimale Anzahl an Spielern in einer Gruppe.
        /// </summary>
        private const int MINIMUM_GROUP_PLAYERS_COUNT = 3;

        #endregion

        #region Public Properties

        /// <summary>
        /// Anzahl der am Turnier teilnehmenden Spieler.
        /// </summary>
        public int AmountOfPlayers => AllPlayers.Count;

        /// <summary>
        /// Liste aller Spieler, die am Turnier teilnehmen.
        /// </summary>
        public List<Player> AllPlayers { get; set; } = new List<Player>();

        /// <summary>
        /// Wird mit Gruppen gespielt?
        /// </summary>
        public bool WithGroupPhase { get; set; } = false;

        #endregion

        #region Methods

        /// <summary>
        /// Einstiegsmethode für die Generierung des Turniers.
        /// </summary>
        public void StartGenerate()
        {
            Console.WriteLine("Welcome to the Tournament Generator!");
            CreateAllPlayers();

            // Überprüfen, ob die Spieleranzahl eine Potenz von zwei ist und Setzen der Variable
            // WithGroupPhase auf den gegenteiligen Wert. Ist die Spieleranzahl keine Potenz von zwei,
            // dann muss zwingend mit Gruppe gespielt werden, da ein Baum sonst nicht generiert werden kann.
            var amountOfPlayersIsPowerOfTwo = CheckIfAmountOfPlayersIsPowerOfTwo();
            WithGroupPhase = !amountOfPlayersIsPowerOfTwo;

            // Nur wenn die Spieleranzahl eine Potenz von zwei ist, sollte die Frage kommen, ob mit oder ohne Gruppen gespielt werden soll.
            // Wurden bloß zwei Spieler angelegt, wird keine Gruppe angelegt,da die Spieleranzahl zu gering für Gruppen ist.
            if (amountOfPlayersIsPowerOfTwo && AmountOfPlayers > MINIMUM_PLAYERS_COUNT)
            {
                Console.WriteLine("Do You want to play with Groupstage or without? Press Y for a GroupPhase" +
                    " or something else for an instant Tournament Tree!");

                if (Console.ReadKey().Key == ConsoleKey.Y)
                {
                    WithGroupPhase = true;
                }
            }

            Console.Clear();
            if (WithGroupPhase)
            {
                if (CheckIfGroupsArePossible())
                {
                    Console.WriteLine("Creating Groups");
                    GroupPhase groupPhase = new GroupPhase(AllPlayers);
                    groupPhase.StartGroupGenerator();
                    // start creating a tree after the groupphase is done
                    Console.WriteLine("Creating a tree out of the Remaining Players.");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();

                    Console.Clear();
                    Console.WriteLine("Creating Tournament Tree!");
                    TournamentTree tree = new TournamentTree(groupPhase.RemainingPlayers);
                    tree.StartTreeGenerator();
                }
                else
                {
                    Console.WriteLine("Can not generate the Groups. You need at least '4' Players!");
                    Console.WriteLine("Restart the Program!");
                }
            }
            else
            {
                // Die Spieleranzahl ist immer eine Potenz von zwei, also kann auch immer
                // ein Turnierbaum generiert werden.
                Console.WriteLine("Creating Tournament Tree!");
                TournamentTree tree = new TournamentTree(AllPlayers);
                tree.StartTreeGenerator();
            }

            // let the Console left open
            Console.ReadLine();
        }

        /// <summary>
        /// Diese Methode überprüft anhand der Spieleranzahl, ob Gruppen angelegt werden können.
        /// </summary>
        /// <returns>true, wenn Spieleranzahl > MINIMUM_GROUP_PLAYERS_COUNT, ansonsten false.</returns>
        private bool CheckIfGroupsArePossible()
        {
            if(AmountOfPlayers >= MINIMUM_GROUP_PLAYERS_COUNT)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Diese Methode wird benötigt, um zu überprüfen, ob ein Turnierbaum erzeugt werden kann. Die Spieleranzahl wird in
        /// einen Binärstring gewandelt und in diesem werden alle 1 gezählt.
        /// </summary>
        /// <returns>true, wenn Spieleranzahl eine Potenz von 2 ist, ansonsten false.</returns>
        private bool CheckIfAmountOfPlayersIsPowerOfTwo()
        {
            var playersCountBinary = Convert.ToString(AmountOfPlayers, 2);
            var count = playersCountBinary.Count(u => u == '1');
            var isPowerOfTwo = count == 1;

            return isPowerOfTwo;
        }

        /// <summary>
        /// Diese Methode wird benötigt, um die Spieler eines Turniers zu erstellen. Anhand der eingegebenen Namen der Spieler
        /// werden Player-Objekte erzeugt und in die Liste AllPlayers geschrieben. Es müssen mindestens zwei Spieler angelegt werden.
        /// </summary>
        private void CreateAllPlayers()
        {
            Console.WriteLine($"Enter player names or STOP if all players has been entered. You have to enter at least {MINIMUM_PLAYERS_COUNT} players!");
            var name = Console.ReadLine();
            while (!String.Equals(name, "STOP", StringComparison.OrdinalIgnoreCase))
            {
                var player = new Player(name);
                AllPlayers.Add(player);
                name = Console.ReadLine();
                
                while (AmountOfPlayers < MINIMUM_PLAYERS_COUNT && String.Equals(name, "STOP", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"You have to enter at least {MINIMUM_PLAYERS_COUNT} players!");
                    name = Console.ReadLine();
                }
            }

            Console.Clear();
            Console.WriteLine("All Players created!");
        }

        #endregion
    }
}
