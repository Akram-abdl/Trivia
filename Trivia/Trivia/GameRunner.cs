using System;
using System.Collections.Generic;

namespace Trivia
{
    public class GameRunner
    {
        private static bool _notAWinner;
        private readonly IConsole console;
        private Random rand;

        public static void Main(string[] args)
        {
            new GameRunner().PlayAGame(new List<string> { "Chet", "Pat", "Sue" });
        }

        private GameRunner()
        {
            console = new SystemConsole();
            rand = new Random();
            console.WriteLine(rand.Next(1, 1).ToString());
        }

        public GameRunner(IConsole console)
        {
            this.console = console;
            rand = new Random();
        }

        // play a game
        public void PlayAGame(List<String> players, int goldCoinsToWin = 6)
        {
            console.WriteLine("Do you want to replace Rock questions with Techno questions? (yes/no): ");
            string userPreference = Console.ReadLine();
            bool replaceRockWithTechno = userPreference.ToLower() == "yes";

            var aGame = new Game(console, rand, replaceRockWithTechno, goldCoinsToWin);

            Game(aGame, players);
        }

        // play a game test
        public void PlayAGameTest(List<String> players, bool rockTechno, int goldCoinsToWin = 6)
        {
            var aGame = new Game(console, rand, rockTechno, goldCoinsToWin);

            Game(aGame, players);
        }

        public void Game(Game aGame, List<String> players)
        {
            try
            {
                foreach (var player in players)
                {
                    aGame.Add(player);
                }

                do
                {
                    bool shouldAnswer = aGame.Roll(rand.Next(5) + 1);

                    if (shouldAnswer)
                    {
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
                    }
                } while (_notAWinner);
            }
            catch (Exception e)
            {
                console.WriteLine(e.Message);
            }
        }

    }
}