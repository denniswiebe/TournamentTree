using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{

    class TournamentGenerator
    {
        public int AmountOfPlayers { get; set; }
        public bool AmountOfPlayersInputCheck { get; set; } = false;
        public List<Player> AllPlayers { get; set; } = new List<Player>();

        public bool WithGroupPhase { get; set; } = false;

        public TournamentGenerator()
        {
            Console.WriteLine("Welcome to the Tournament Generator!");
            ConfigureAmountOfPlayers();
            Console.WriteLine("Enter now the Names for the Players!");
            CreatePlayers();
            Console.WriteLine("All Players created!");

            Console.WriteLine("Do You want to play with Groupstage or without? Press Y for a GroupPhase or something else for an instant Tournament Tree!");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.WriteLine("You Pressed Y");
                WithGroupPhase = true;
            }
            else
            {
                Console.WriteLine("You decided for the option: no Groupphase");
            }

            if (WithGroupPhase)
            {
                Console.WriteLine("Creating Groups");
            }
            else
            {
                Console.WriteLine("Creating Tournament Tree!");
                TournamentTree tree = new TournamentTree(AllPlayers);
            }


            Console.ReadLine();
        }

        public void ConfigureAmountOfPlayers()
        {
            do
            {
                Console.WriteLine("How many Players do you want to be in the Tournament?");
                Console.WriteLine("Keep in mind that a Tournament Tree has to be 2, 4, 8, 16, 32 or 64 Players");
                string amountOfPlayersInput = Console.ReadLine();
                if (int.TryParse(amountOfPlayersInput, out int result))
                {
                    if (result > 2)
                    {
                        AmountOfPlayers = result;
                        AmountOfPlayersInputCheck = true;
                    }
                    else
                    {
                        Console.WriteLine("Amount of Players must be atleast '2'!");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a Number. Wrong input!");
                }
            } while (!AmountOfPlayersInputCheck);
        }

        public void CreatePlayers()
        {
            for (int i = 1; i <= AmountOfPlayers; i++)
            {
                string playerNameInput;
                do
                {
                    Console.WriteLine("Spielername " + i + ": ");
                    playerNameInput = Console.ReadLine();
                } while (CheckIfStringIsEmpty(playerNameInput));
                Player newPlayer = new Player(playerNameInput);
                AllPlayers.Add(newPlayer);
            }
        }

        public bool CheckIfStringIsEmpty(string str)
        {
            return str.Equals("");
        }

    }
}
