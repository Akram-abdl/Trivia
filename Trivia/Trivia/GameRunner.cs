using System;
using System.Collections.Generic;

namespace Trivia
{
    public class GameRunner
    {
        private static bool _notAWinner;
        private readonly IConsole console;

        public static void Main(string[] args)
        {
            Player player1 = new Player("Chet");
            Player player2 = new Player("Pat");
            Player player3 = new Player("Sue");
            new GameRunner().PlayAGame(new List<Player> { player1, player2, player3 });
        }

        private GameRunner()
        {
            console = new SystemConsole();
        }

        public GameRunner(IConsole console)
        {
            this.console = console;
        }

        // play a game
        public void PlayAGame(List<Player> players, int goldCoinsToWin = 6)
        {
            console.WriteLine("Do you want to replace Rock questions with Techno questions? (yes/no): ");
            string userPreference = Console.ReadLine();
            bool replaceRockWithTechno = userPreference.ToLower() == "yes";

            var aGame = new Game(console, replaceRockWithTechno, goldCoinsToWin);

            Game(aGame, players);
        }

        // play a game test
        public void PlayAGameTest(List<Player> players, bool rockTechno, int goldCoinsToWin = 6)
        {
            var aGame = new Game(console, rockTechno, goldCoinsToWin);

            Game(aGame, players);
        }

        public void Game(Game aGame, List<Player> players)
        {
            try
            {
                foreach (Player player in players)
                {
                    aGame.Add(player);
                }

                var rand = new Random();

                do
                {
                    bool shouldAnswer = aGame.Roll(rand.Next(5) + 1);

                    if (shouldAnswer)
                    {
                        String userAnswer = aGame.AskBoolQuestion();
                        
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
                           ;
                           _notAWinner = aGame.RemovePlayer(aGame.GetCurrentPlayer());
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