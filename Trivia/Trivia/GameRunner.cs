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
            Player player1 = new Player("Chet");
            Player player2 = new Player("Pat");
            Player player3 = new Player("Sue");
            new GameRunner().PlayAGame(new List<Player> { player1, player2, player3 });
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
        public void PlayAGame(List<Player> players, int goldCoinsToWin = 6)
        {
            console.WriteLine("Do you want to replace Rock questions with Techno questions? (yes/no): ");
            string userPreference = Console.ReadLine();
            bool replaceRockWithTechno = userPreference.ToLower() == "yes";

            var aGame = new Game(console, rand, replaceRockWithTechno, goldCoinsToWin);

            Game(aGame, players);
        }

        // play a game test
        public void PlayAGameTest(List<Player> players, bool rockTechno, int goldCoinsToWin = 6)
        {
            var aGame = new Game(console, rand, rockTechno, goldCoinsToWin);

            Game(aGame, players);
        }

        public void Game(Game aGame, List<Player> players)
        {
            string message;
            try
            {
                do {
                    foreach(Player player in players) {
                        aGame.Add(player);
                    }

                    do {
                        bool shouldAnswer = aGame.Roll(rand.Next(5) + 1);

                        if (shouldAnswer) {
                            console.WriteLine("Do you want to answer the question? (yes/leave): ");
                            string userAnswer = Console.ReadLine().ToLower();

                            if (userAnswer == "yes") {
                                if (rand.Next(9) == 7) {
                                    _notAWinner = aGame.WrongAnswer();
                                } else {
                                    _notAWinner = aGame.WasCorrectlyAnswered();
                                }
                            } else if (userAnswer == "leave") {
                                ;
                                _notAWinner = aGame.RemovePlayer(aGame.GetCurrentPlayer());
                            }
                        }
                    } while (_notAWinner);
                    Console.WriteLine(" Voulez vous rejouer la partie avec les mêmes paramètres ? (y/n)");
                    message = Console.ReadLine();
                } while (message == "y");
            }
            catch (Exception e)
            {
                console.WriteLine(e.Message);
            }
        }

    }
}