using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentTree
{
    class ChooseHelper : TournamentGenerator
    {

        public new int AmountOfPlayers { get; set; }

        public ChooseHelper(int amountOfPlayers)
        {
            AmountOfPlayers = amountOfPlayers;
        }

        public string Help()
        {
            string help;
            if (AmountOfPlayers < 2)
            {
                help = "The amount of Players are " + AmountOfPlayers + ", which means you need to add more Players";
            }
            else if (CheckIfAmountOfPlayersIsPowerOfTwo(AmountOfPlayers) && AmountOfPlayers >= 2)
            {
                help = "The amount of Players are " + AmountOfPlayers + ", which means you can choose between Groupphase or without.";
            }
            else
            {
                help = "The amount of Players are " + AmountOfPlayers + ", which means you have to play a Groupphase first";
            }
            return help;
        }

        public string Suggestion()
        {
            string suggestion = "We recommend choosing these Options for the following Games:\n";
            suggestion += "Super Smash Bros.: Tournament Tree with Double KO\n";
            suggestion += "FIFA             : Grouphase and standard Tournament\n";
            suggestion += "Billiard         : Tournament Tree with Double KO\n";
            suggestion += "Dart             : standard Tournament Tree\n";
            return suggestion;
        }
    }
}
