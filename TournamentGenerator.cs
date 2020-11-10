﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{

    class TournamentGenerator
    {
        public int AmountOfPlayers { get; set; }

        public List<Player> AllPlayers { get; set; } = new List<Player>();

        public bool WithGroupPhase { get; set; } = false;

        public void StartGenerate()
        {
            Console.WriteLine("Welcome to the Tournament Generator!");
            ConfigureAmountOfPlayers();
            CreatePlayers();

            Console.WriteLine("Do You want to play with Groupstage or without? Press Y for a GroupPhase" +
                " or something else for an instant Tournament Tree!");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                WithGroupPhase = true;
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
                if (CheckIfTreeIsPossible())
                {
                    Console.WriteLine("Creating Tournament Tree!");
                    TournamentTree tree = new TournamentTree(AllPlayers);
                    tree.StartTreeGenerator();
                }
                else
                {
                    Console.WriteLine("Can not generate a tree out of a Number which is not in power of '2'!");
                    Console.WriteLine("Restart the Program!");
                }
            }

            // let the Console left open
            Console.ReadLine();
        }

        private bool CheckIfGroupsArePossible()
        {
            if(AmountOfPlayers >= 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckIfTreeIsPossible()
        {
            // check if Amount of Players is in power of '2'
            int temp = AmountOfPlayers;
            while(temp != 1)
            {
                if(temp % 2 != 0)
                {
                    return false;
                }

                temp /= 2;
            }
            return true;
        }

        private void ConfigureAmountOfPlayers()
        {
            bool amountOfPlayersInputCheck = false;
            do
            {
                Console.WriteLine("How many Players do you want to be in the Tournament?");
                Console.WriteLine("Keep in mind that a Tournament Tree has to be 2, 4, 8, 16, 32 or 64 Players");
                Console.WriteLine("For a GroupPhase you need at least '4' Players");
                string amountOfPlayersInput = Console.ReadLine();
                if (int.TryParse(amountOfPlayersInput, out int result))
                {
                    if (result >= 2 && result <= 64)
                    {
                        AmountOfPlayers = result;
                        amountOfPlayersInputCheck = true;
                    }
                    else
                    {
                        Console.WriteLine("Amount of Players must be atleast '2' and " +
                            "maximum '64'.");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a Number. Wrong input!");
                }
            } while (!amountOfPlayersInputCheck);
        }

        private void CreatePlayers()
        {
            Console.WriteLine("Enter now the Names for the Players!");
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
            Console.Clear();
            Console.WriteLine("All Players created!");
        }

        private bool CheckIfStringIsEmpty(string str)
        {
            return str.Equals("");
        }

    }
}
