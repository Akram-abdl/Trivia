using System;
using System.Collections.Generic;

namespace Trivia
{
    public class GameRunner
    {
        private static bool _notAWinner;

        public static void Main(string[] args)
        {
            new GameRunner().PlayAGame(new List<string> {"Chet", "Pat", "Sue"});
        }
        
        // play a game
        public void PlayAGame(List<String> players)
        {
            
            Console.WriteLine("Do you want to replace Rock questions with Techno questions? (yes/no): ");
            string userPreference = Console.ReadLine();
            bool replaceRockWithTechno = userPreference.ToLower() == "yes";

            var aGame = new Game(replaceRockWithTechno);

            foreach (var player in players)
            {
                aGame.Add(player);
            }

            var rand = new Random();

            do
            {
                aGame.Roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    _notAWinner = aGame.WrongAnswer();
                }
                else
                {
                    _notAWinner = aGame.WasCorrectlyAnswered();
                }
            } while (_notAWinner);
        }
    }
}