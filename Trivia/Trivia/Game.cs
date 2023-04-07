using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly List<string> _players = new();

        private readonly int[] _places = new int[6];
        private readonly int[] _purses = new int[6];

        private readonly bool[] _inPenaltyBox = new bool[6];
        private readonly bool _replaceRockWithTechno;

        private readonly LinkedList<string> _technoQuestions = new();
        private readonly LinkedList<string> _popQuestions = new();
        private readonly LinkedList<string> _scienceQuestions = new();
        private readonly LinkedList<string> _sportsQuestions = new();
        private readonly LinkedList<string> _rockQuestions = new();

        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;
        private IConsole console;

        // constructor
        public Game(IConsole console, bool replaceRockWithTechno = false)
        {
            this.console = console;
            _replaceRockWithTechno = replaceRockWithTechno;
            for (var i = 0; i < 50; i++)
            {
                _popQuestions.AddLast(CreatePopQuestion(i));
                _scienceQuestions.AddLast(CreateScienceQuestion(i));
                _sportsQuestions.AddLast(CreateSportsQuestion(i));
                if (replaceRockWithTechno)
                {
                    _technoQuestions.AddLast(CreateTechnoQuestion(i));
                }
                else
                {
                    _rockQuestions.AddLast(CreateRockQuestion(i));
                }
            }
        }

        // create a question for each category
        public string CreatePopQuestion(int index)
        {
            return "Pop Question " + index;
        }
        
        public string CreateScienceQuestion(int index)
        {
            return "Science Question " + index;
        }
        
        public string CreateSportsQuestion(int index)
        {
            return "Sports Question " + index;
        }
        public string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }
        public string CreateTechnoQuestion(int index)
        {
            return "Techno Question " + index;
        }

        // check if the game is playable
        public bool IsPlayable()
        {
            return (HowManyPlayers() >= 2);
        }

        // add a player to the game
        public bool Add(string playerName)
        {
            if (HowManyPlayers() >= 6)
                throw new TooManyPlayersException();

            _players.Add(playerName);
            _places[HowManyPlayers() - 1] = 0;
            _purses[HowManyPlayers() - 1] = 0;
            _inPenaltyBox[HowManyPlayers() - 1] = false;

            this.console.WriteLine(playerName + " was added");
            this.console.WriteLine("They are player number " + _players.Count);
            return true;
        }

        // how many players are in the game
        public int HowManyPlayers()
        {
            return _players.Count;
        }
        public bool RemovePlayer(string playerName)
        {
            int playerIndex = _players.IndexOf(playerName);
            if (playerIndex == -1)
            {
                throw new InvalidOperationException("Player not found");
            }

            _players.RemoveAt(playerIndex);
            int length = _players.Count - playerIndex - 1;
            if (length > 0)
            {
                Array.Copy(_places, playerIndex + 1, _places, playerIndex, length);
                Array.Copy(_purses, playerIndex + 1, _purses, playerIndex, length);
                Array.Copy(_inPenaltyBox, playerIndex + 1, _inPenaltyBox, playerIndex, length);
            }


            if (_currentPlayer >= playerIndex)
            {
                _currentPlayer = _currentPlayer > 0 ? _currentPlayer - 1 : _players.Count - 1;
            }
            this.console.WriteLine(playerName + " has left the game.");

            return IsPlayable(); // return whether the game is still playable after removing the player
        }

        
        public string GetCurrentPlayerName()
        {
            return _players[_currentPlayer];
        }
        // roll the dice
        public void Roll(int roll)
        {
            if (HowManyPlayers() < 2)
                throw new NotEnoughPlayersException();

            this.console.WriteLine(_players[_currentPlayer] + " is the current player");
            this.console.WriteLine("They have rolled a " + roll);

            if (_inPenaltyBox[_currentPlayer])
            {
                // impair number
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    this.console.WriteLine(_players[_currentPlayer] + " is getting out of the penalty box");
                    _places[_currentPlayer] += roll;
                    if (_places[_currentPlayer] > 11) _places[_currentPlayer] -= 12;

                    this.console.WriteLine(_players[_currentPlayer]
                            + "'s new location is "
                            + _places[_currentPlayer]);
                    this.console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    this.console.WriteLine(_players[_currentPlayer] + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                _places[_currentPlayer] += roll;
                if (_places[_currentPlayer] > 11) _places[_currentPlayer] -= 12;

                this.console.WriteLine(_players[_currentPlayer]
                        + "'s new location is "
                        + _places[_currentPlayer]);
                this.console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }
        }

        // ask a question
        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                this.console.WriteLine(_popQuestions.First());
                _popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                this.console.WriteLine(_scienceQuestions.First());
                _scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                this.console.WriteLine(_sportsQuestions.First());
                _sportsQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Rock" && !_technoQuestions.Any())
            {
                this.console.WriteLine(_rockQuestions.First());
                _rockQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Rock" && _technoQuestions.Any())
            {
                this.console.WriteLine(_technoQuestions.First());
                _technoQuestions.RemoveFirst();
            }
        }

        // current category
        private string CurrentCategory()
        {
            if (_places[_currentPlayer] == 0 || _places[_currentPlayer] == 4 ||_places[_currentPlayer] == 8) return "Pop";
            if (_places[_currentPlayer] == 1 ||_places[_currentPlayer] == 5||_places[_currentPlayer] == 9) return "Science";
            if (_places[_currentPlayer] == 2||_places[_currentPlayer] == 6||_places[_currentPlayer] == 10) return "Sports";
            return _replaceRockWithTechno ? "Techno" : "Rock";
        }

        // if the player answered correctly
        public bool WasCorrectlyAnswered()
        {
            if (_inPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    this.console.WriteLine("Answer was correct!!!!");
                    _purses[_currentPlayer]++;
                    this.console.WriteLine(_players[_currentPlayer]
                                           + " now has "
                                           + _purses[_currentPlayer]
                                           + " Gold Coins.");

                    var winner = DidPlayerWin();
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;

                    return winner;
                }
                else
                {
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;
                    return true;
                }
            }
            else
            {
                this.console.WriteLine("Answer was corrent!!!!");
                _purses[_currentPlayer]++;
                this.console.WriteLine(_players[_currentPlayer]
                                       + " now has "
                                       + _purses[_currentPlayer]
                                       + " Gold Coins.");

                var winner = DidPlayerWin();
                _currentPlayer++;
                if (_currentPlayer == _players.Count) _currentPlayer = 0;

                return winner;
            }
        }

        // if the player answered incorrectly
        public bool WrongAnswer()
        {
            this.console.WriteLine("Question was incorrectly answered");
            this.console.WriteLine(_players[_currentPlayer] + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            return true;
        }

        // check if the player won
        private bool DidPlayerWin()
        {
            return !(_purses[_currentPlayer] == 6);
        }
    }

}
