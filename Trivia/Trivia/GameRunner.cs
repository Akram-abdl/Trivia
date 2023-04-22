using System;
using System.Collections.Generic;

namespace Trivia
{
    public class GameRunner
    {
        private static bool _notAWinner;
        private readonly IConsole console;
        private List<Player> winners = new List<Player>();

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
        public void PlayAGame(List<Player> players, int goldCoinsToWin = 2)
        {
            if (players.Count < 2)
            {
                console.WriteLine("Il n'y a pas assez de joueurs pour démarrer une partie, ajoutez-en au moins 2.");
                return;
            }

            console.WriteLine("Do you want to replace Rock questions with Techno questions? (yes/no): ");
            string userPreference = Console.ReadLine();
            bool replaceRockWithTechno = userPreference.ToLower() == "yes";

            var aGame = new Game(console, replaceRockWithTechno, goldCoinsToWin);

            Game(aGame, players);
        }

        // play a game test
        public void PlayAGameTest(List<Player> players, bool rockTechno, int goldCoinsToWin = 2)
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

                while (players.Count > 0 && winners.Count < 3)
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
                                aGame.WrongAnswer();
                            }
                            else
                            {
                                Player winner = aGame.WasCorrectlyAnswered();
                                if (winner != null)
                                {
                                    winners.Add(winner);
                                    aGame.RemovePlayer(winner);
                                }
                            }
                        }
                        else if (userAnswer == "leave")
                        {
                            Player currentPlayer = aGame.GetCurrentPlayer();
                            aGame.RemovePlayer(currentPlayer);
                        }
                    }
                }

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
