using System;
using System.Collections.Generic;

namespace Trivia
{
    public class GameRunner
    {
        private static bool _notAWinner;
        private readonly IConsole console;
        private readonly bool useRockQuestions;

        public static void Main(string[] args)
        {
            new GameRunner().PlayAGame(new List<string> {"Chet", "Pat", "Sue"});
        }
        
        private GameRunner()
        {
            console = new SystemConsole();
            useRockQuestions = Random.Shared.Next() % 2 == 1;
        }
        
        public GameRunner(IConsole console, bool useRockQuestions)
        {
            this.console = console;
            this.useRockQuestions = useRockQuestions;
        }
        
        // play a game
        public void PlayAGame(List<String> players)
        {
            console.WriteLine("Do you want to replace Rock questions with Techno questions? (yes/no): ");
            string userPreference = Console.ReadLine();
            bool replaceRockWithTechno = userPreference.ToLower() == "yes";

            var aGame = new Game(console, replaceRockWithTechno);

            foreach (var player in players)
            {
                aGame.Add(player);
            }

            var rand = new Random();

            do
            {
                aGame.Roll(rand.Next(5) + 1);

                console.WriteLine("Do you want to answer the question? (yes/leave): ");
                string userAnswer = Console.ReadLine().ToLower();

                if (userAnswer == "yes")
                {
                    if (rand.Next(9) == 7)
                    {
                        _notAWinner = aGame.WrongAnswer();
                    }
                    else
                    {
                        _notAWinner = aGame.WasCorrectlyAnswered();
                    }
                }
                else if (userAnswer == "leave")
                {
                    string currentPlayerName = aGame.GetCurrentPlayerName();
                    _notAWinner = aGame.RemovePlayer(currentPlayerName);
                }
            } while (_notAWinner);
        }


    }
}