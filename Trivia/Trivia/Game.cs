﻿using System;
using System.Collections.Generic;
using System.Linq;
using Trivia.Exceptions;

namespace Trivia
{
    public class Game
    {
        private List<Player> _players = new List<Player>();

        private readonly int[] _places = new int[6];
        private readonly int[] _purses = new int[6];
        private int[] corectanswerRow = new int[6];
        private readonly bool[] _inPenaltyBox = new bool[6];
        private readonly bool _replaceRockWithTechno;

        private readonly LinkedList<string> _technoQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();

        private int _currentPlayer;
        private readonly int _goldCoinsToWin;
        private bool _isGettingOutOfPenaltyBox;
        private IConsole console;
        private int currentQuestionIndex = 50;

        // constructor
        public Game(IConsole console, bool replaceRockWithTechno = false, int goldCoinsToWin = 6)
        {
            this.console = console;
            _replaceRockWithTechno = replaceRockWithTechno;
            _goldCoinsToWin = goldCoinsToWin;
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
        public bool Add(Player player)
        {
            if (HowManyPlayers() == 6)
                throw new Exception(Messages.TooManyPlayerException);
            
            _players.Add(player);
            _places[HowManyPlayers() - 1] = 0;
            _purses[HowManyPlayers() - 1] = 0;
            corectanswerRow[HowManyPlayers() - 1] = 0;
            _inPenaltyBox[HowManyPlayers()-1] = false;
            
            this.console.WriteLine(player + " was added");
            this.console.WriteLine("They are player number " + _players.Count);
            return true;
        }

        // how many players are in the game at this moment
        public int HowManyPlayers()
        {
            return _players.Count;
        }
        
        public bool RemovePlayer(Player player)
        {
            int playerIndex = _players.IndexOf(player);
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
            this.console.WriteLine(player + " has left the game.");

            return IsPlayable(); // return whether the game is still playable after removing the player
        }

        
        public Player GetCurrentPlayer()
        {
            return _players[_currentPlayer];
        }
        // roll the dice
        public bool Roll(int roll)
        {
            if (HowManyPlayers() < 2)
                throw new Exception(Messages.NotEnoughPlayerException);

            this.console.WriteLine(_players[_currentPlayer] + " is the current player");
            this.console.WriteLine(_players[_currentPlayer].name + " is the current player");
            this.console.WriteLine("They have rolled a " + roll);

            if (_inPenaltyBox[_currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    this.console.WriteLine(_players[_currentPlayer].name + " is getting out of the penalty box");
                    _inPenaltyBox[_currentPlayer] = false;
                    _places[_currentPlayer] += roll;
                    if (_places[_currentPlayer] > 11) _places[_currentPlayer] -= 12;

                    this.console.WriteLine(_players[_currentPlayer].name
                            + "'s new location is "
                            + _places[_currentPlayer]);
                    this.console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    this.console.WriteLine(_players[_currentPlayer].name + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
                return _isGettingOutOfPenaltyBox;
            }
            else
            {
                _places[_currentPlayer] += roll;
                if (_places[_currentPlayer] > 11) _places[_currentPlayer] -= 12;

                this.console.WriteLine(_players[_currentPlayer].name
                        + "'s new location is "
                        + _places[_currentPlayer]);
                this.console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
                return true;
            }
        }

        // ask a question
        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                this.console.WriteLine(_popQuestions.First());
                _popQuestions.AddLast(CreatePopQuestion(currentQuestionIndex++));
                _popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                this.console.WriteLine(_scienceQuestions.First());
                _scienceQuestions.AddLast(CreateScienceQuestion(currentQuestionIndex++));
                _scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                this.console.WriteLine(_sportsQuestions.First());
                _sportsQuestions.AddLast(CreateSportsQuestion(currentQuestionIndex++));
                _sportsQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Rock" && !_technoQuestions.Any())
            {
                this.console.WriteLine(_rockQuestions.First());
                _rockQuestions.AddLast(CreateRockQuestion(currentQuestionIndex++));
                _rockQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Techno")
            {
                this.console.WriteLine(_technoQuestions.First());
                _technoQuestions.AddLast(CreateTechnoQuestion(currentQuestionIndex++));
                _technoQuestions.RemoveFirst();
            }

        }

        // current category

        public bool askJoker()

        {
            bool val = false;
            if (_players[_currentPlayer].nbJoker == 1)
            {
                console.WriteLine("Do you want to use your joker ?");
                
                if (Console.ReadLine() == 'y'.ToString()) 
                {
                    _players[_currentPlayer].nbJoker++;
                    _players[_currentPlayer].use = true;
                   val = true;
                }
                

            }
            return val;
            
        }
        private string CurrentCategory()
        {
            if (_places[_currentPlayer] == 0 || _places[_currentPlayer] == 4 || _places[_currentPlayer] == 8) return "Pop";
            if (_places[_currentPlayer] == 1 || _places[_currentPlayer] == 5 || _places[_currentPlayer] == 9) return "Science";
            if (_places[_currentPlayer] == 2 || _places[_currentPlayer] == 6 || _places[_currentPlayer] == 10) return "Sports";
            if (_technoQuestions.Any()) return "Techno";
            return "Rock";
        }


        // if the player answered correctly
        public Player WasCorrectlyAnswered()
        {
            Player winner = null;

            if (_inPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    int temp = int.Parse(corectanswerRow[_currentPlayer].ToString());
                    corectanswerRow[_currentPlayer] = temp + 1;
                    int bourse = int.Parse(_purses[_currentPlayer].ToString());
                    _purses[_currentPlayer] = bourse + temp;

                    console.WriteLine("Answer was correct!!!!");
                    _purses[_currentPlayer]++;
                    console.WriteLine(_players[_currentPlayer].name
                                      + " now has "
                                      + _purses[_currentPlayer]
                                      + " Gold Coins.");

                    if (DidPlayerWin())
                    {
                        winner = _players[_currentPlayer];
                    }

                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;
                }
                else
                {
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;
                }
            }
            else
            {
                console.WriteLine("Answer was correct!!!!");
                _purses[_currentPlayer]++;
                console.WriteLine($"{_players[_currentPlayer]} now has {_purses[_currentPlayer]} Gold Coins.");

                if (DidPlayerWin())
                {
                    winner = _players[_currentPlayer];
                }

                _currentPlayer++;
                if (_currentPlayer == _players.Count) _currentPlayer = 0;
            }

            return winner;
        }


        // if the player answered wrong
        public void WrongAnswer()
        {
            console.WriteLine("Question was incorrectly answered");
            console.WriteLine($"{_players[_currentPlayer]} was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
        }

        private bool DidPlayerWin()
        {
            if (_purses[_currentPlayer] == _goldCoinsToWin)
            {
                console.WriteLine($"{_players[_currentPlayer]} has won the game with {_purses[_currentPlayer]} Gold Coins!");
                return true;
            }
            else
            {
                return false;
            }
        }


    }

}
