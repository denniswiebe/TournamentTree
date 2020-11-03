using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    class Match
    {
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }

        public Match(Player playerOne, Player playerTwo)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
        }

        public void PlayMatch()
        {
            Console.WriteLine("Match between: " + PlayerOne.PlayerName + " VS " + PlayerTwo.PlayerName);
            ValidateMatch();
        }

        private void ValidateMatch()
        {
            Console.WriteLine("What is the result of the Match? (Example: 2 3)");
            string input = Console.ReadLine();
            string[] splitInput = input.Split(" ");
            int firstNumber = int.Parse(splitInput[0]);
            int secondNumber = int.Parse(splitInput[1]);
            if (firstNumber > secondNumber)
            {
                PlayerOne.Wins++;
            }
            else if (secondNumber > firstNumber)
            {
                PlayerTwo.Wins++;
            }
            else
            {
                PlayerOne.Ties++;
                PlayerTwo.Ties++;
            }

            // add/ subtract goals to GoalDifference
            PlayerOne.GoalDifference += firstNumber;
            PlayerOne.GoalDifference -= secondNumber;
            PlayerTwo.GoalDifference -= firstNumber;
            PlayerTwo.GoalDifference += secondNumber;
        }
    }
}
