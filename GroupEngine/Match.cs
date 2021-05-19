using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree
{
    public class Match
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
            string input;
            do
            {
                input = Console.ReadLine();        
            } while (!CheckInputOfMatch(input));

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

        public bool CheckInputOfMatch(string input)
        {
            string trimmedInput = input.Trim();
            string[] splitInput = trimmedInput.Split(" ");
            // Input must be Number Space Number, => Check if there are two values around space
            if (splitInput.Length != 2)
            {
                Console.WriteLine("Wrong input! You need 2 Values!");
                return false;
            }
            // Check if first value is a number
            if (!int.TryParse(splitInput[0], out _))
            {
                Console.WriteLine("Wrong input! First Value must be a Number!");
                return false;
            }
            // Check if second Value is a number
            if (!int.TryParse(splitInput[1], out _))
            {
                Console.WriteLine("Wrong input! Second Value must be a Number!");
                return false;
            }

            return true;
        }

        public void ChangeHomeAndAway()
        {
            Player temp = PlayerOne;
            PlayerOne = PlayerTwo;
            PlayerTwo = temp;
        }
    }
}
