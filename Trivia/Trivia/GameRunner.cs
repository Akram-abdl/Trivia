using System;
using System.Collections.Generic;

namespace Trivia
{
    public class GameRunner
    {
        private static bool _notAWinner;
        private readonly IConsole console;

        private List<Player> winners = new List<Player>();
        private Random rand;


        public static void Main(string[] args)
        {
            Player player1 = new Player("Chet");
            Player player2 = new Player("Pat");
            Player player3 = new Player("Sue");
            Player player4 = new Player("Sue2");
            new GameRunner().PlayAGame(new List<Player> { player1, player2, player3, player4 });
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
                int numPlayersInLeaderboard = 0;
                do
                {
                    foreach (Player player in players)
                    {
                        aGame.Add(player);
                    }

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
                                _notAWinner = aGame.RemovePlayer(aGame.GetCurrentPlayer());
                            }
                        }
                    } while (_notAWinner);

                    // Check if the winning player is already in the winners list
                    Player winner = aGame.GetCurrentPlayer();
                    if (!winners.Contains(winner))
                    {
                        winners.Add(winner);
                        numPlayersInLeaderboard++;
                    }

                    if (numPlayersInLeaderboard >= 3 || players.Count == 0)
                    {
                        break;
                    }

                    
                    message = aGame.AskReGameQuestion();
                } while (message == "y");

                console.WriteLine("Game Over! Here is the leaderboard:");
                for (int i = 0; i < winners.Count; i++)
                {
                    console.WriteLine($"{i + 1}. {winners[i].name}");
                }
            }
            catch (Exception e)
            {
                console.WriteLine(e.Message);
            }
        }
    }
}