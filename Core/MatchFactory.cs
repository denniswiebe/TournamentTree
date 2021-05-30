using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentTree.Core
{
    public class MatchFactory
    {
        public void PlayMatch(Match match)
        {
            Console.WriteLine("Match between: " + match.PlayerOne.PlayerName + " VS " + match.PlayerTwo.PlayerName);
            ValidateMatch(match);
        }

        private void ValidateMatch(Match match)
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
                match.PlayerOne.Wins++;
            }
            else if (secondNumber > firstNumber)
            {
                match.PlayerTwo.Wins++;
            }
            else
            {
                match.PlayerOne.Ties++;
                match.PlayerTwo.Ties++;
            }

            // add/ subtract goals to GoalDifference
            match.PlayerOne.GoalDifference += firstNumber;
            match.PlayerOne.GoalDifference -= secondNumber;
            match.PlayerTwo.GoalDifference -= firstNumber;
            match.PlayerTwo.GoalDifference += secondNumber;

            // Add as match for the Log Engine
            TournamentGroupLog.AddMatch(TournamentGroupLogMatch.Create(match.PlayerOne.PlayerName.Title, match.PlayerTwo.PlayerName.Title, firstNumber, secondNumber));
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

        public void ChangeHomeAndAway(Match match)
        {
            Player temp = match.PlayerOne;
            match.PlayerOne = match.PlayerTwo;
            match.PlayerTwo = temp;
        }
    }
}
