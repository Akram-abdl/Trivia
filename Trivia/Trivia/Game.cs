using System;
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
        private readonly int[] _timesInPrison = new int[6];
        private readonly int[] _turnInPrison = new int[6];
        private int[] corectanswerRow = new int[6];
        private readonly bool[] _inPenaltyBox = new bool[6];
        private readonly bool _replaceRockWithTechno;
        private const int HowManyPlaces = 15;
        private readonly LinkedList<string> _technoQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();
        // expansion pack
        private readonly LinkedList<string> _rapQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _philosophyQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _literatureQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _geographyQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _peopleQuestions = new LinkedList<string>();


        private int _currentPlayer;
        private readonly int _goldCoinsToWin;
        private bool _isGettingOutOfPenaltyBox;
        private Random rand;
        private IConsole console;
        private int currentQuestionIndex = 50;
        private string message;

        // constructor
        public Game(IConsole console, Random rand, bool replaceRockWithTechno = false, int goldCoinsToWin = 6)
        {
            this.console = console;
            _replaceRockWithTechno = replaceRockWithTechno;
            _goldCoinsToWin = goldCoinsToWin;
            this.rand = rand;
            
            for (var i = 0; i < 50; i++)
            {
                _popQuestions.AddLast(CreatePopQuestion(i));
                _scienceQuestions.AddLast(CreateScienceQuestion(i));
                _sportsQuestions.AddLast(CreateSportsQuestion(i));
                _rapQuestions.AddLast(CreateRapQuestion(i));
                _philosophyQuestions.AddLast(CreatePhilosophyQuestion(i));
                _literatureQuestions.AddLast(CreateLiteratureQuestion(i));
                _geographyQuestions.AddLast(CreateGeographyQuestion(i));
                _peopleQuestions.AddLast(CreatePeopleQuestion(i));
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
        //expansion pack subjects
        
        public string CreateRapQuestion(int index)
        {
            return "Rap Question " + index;
        }

        public string CreatePhilosophyQuestion(int index)
        {
            return "Philosophy Question " + index;
        }

        public string CreateLiteratureQuestion(int index)
        {
            return "Literature Question " + index;
        }

        public string CreateGeographyQuestion(int index)
        {
            return "Geography Question " + index;
        }

        public string CreatePeopleQuestion(int index)
        {
            return "People Question " + index;
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
            _timesInPrison[HowManyPlayers() - 1] = 0;
            _turnInPrison[HowManyPlayers() - 1] = 0;
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
            {
                throw new Exception(Messages.NotEnoughPlayerException);
            }

            this.console.WriteLine(" ");
            this.console.WriteLine(_players[_currentPlayer].name + " is the current player");
            this.console.WriteLine("They have rolled a " + roll);

            if (_inPenaltyBox[_currentPlayer])
            {
                if (rand.Next(1,
                        (int)(_timesInPrison[_currentPlayer] * (1.0 - (0.1 * _turnInPrison[_currentPlayer])))) == 1)
                {
                    _isGettingOutOfPenaltyBox = true;

                    this.console.WriteLine(_players[_currentPlayer].name + " is getting out of the penalty box");
                    _inPenaltyBox[_currentPlayer] = false;
                    _places[_currentPlayer] += roll;
                    if (_places[_currentPlayer] > HowManyPlaces) _places[_currentPlayer] -= HowManyPlaces+1;

                    this.console.WriteLine(_players[_currentPlayer].name
                                           + " is new location is "
                                           + _places[_currentPlayer]);
                    this.console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    this.console.WriteLine(_players[_currentPlayer].name + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                    _turnInPrison[_currentPlayer]++;
                }
                return _isGettingOutOfPenaltyBox;
            }
            else
            {
                _places[_currentPlayer] += roll;
                if (_places[_currentPlayer] > HowManyPlaces) _places[_currentPlayer] -= HowManyPlaces+1;

                this.console.WriteLine(_players[_currentPlayer].name
                                       + " is new location is "
                                       + _places[_currentPlayer]);
                this.console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
                return true;
            }
        }

        public String AskBoolQuestion()
        {
            if (_players[_currentPlayer].askYesQuestion == 0)
            {
                console.WriteLine("Do you want to answer the question? (yes/leave): ");
                return Console.ReadLine().ToLower();
            }
            else if (_players[_currentPlayer].askYesQuestion == 1)
            {
                return "yes";
            }
            else
            {
                return "leave";
            }
        }

        public String AskReGameQuestion()
        {
            if (_players[_currentPlayer].reGameQuestion == 0)
            {
                console.WriteLine(" Voulez vous rejouer la partie avec les mêmes paramètres ? (y/n)");
                return Console.ReadLine().ToLower();
            }
            else if (_players[_currentPlayer].askYesQuestion == 1)
            {
                return "y";
            }
            else
            {
                return "n";
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
            if (CurrentCategory() == "Rap")
            {
                this.console.WriteLine(_rapQuestions.First());
                _rapQuestions.AddLast(CreateRapQuestion(currentQuestionIndex++));
                _rapQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Philosophy")
            {
                this.console.WriteLine(_philosophyQuestions.First());
                _philosophyQuestions.AddLast(CreatePhilosophyQuestion(currentQuestionIndex++));
                _philosophyQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Literature")
            {
                this.console.WriteLine(_literatureQuestions.First());
                _literatureQuestions.AddLast(CreateLiteratureQuestion(currentQuestionIndex++));
                _literatureQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Geography")
            {
                this.console.WriteLine(_geographyQuestions.First());
                _geographyQuestions.AddLast(CreateGeographyQuestion(currentQuestionIndex++));
                _geographyQuestions.RemoveFirst();
            }

            if (CurrentCategory() == "People")
            {
                this.console.WriteLine(_peopleQuestions.First());
                _peopleQuestions.AddLast(CreatePeopleQuestion(currentQuestionIndex++));
                _peopleQuestions.RemoveFirst();
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
            if (_places[_currentPlayer] == 0 || _places[_currentPlayer] == 4 ) return "Pop";
            if (_places[_currentPlayer] == 1 || _places[_currentPlayer] == 5 ) return "Science";
            if (_places[_currentPlayer] == 2 || _places[_currentPlayer] == 6 ) return "Sports";
            if (_places[_currentPlayer] == 3 || _places[_currentPlayer] == 7) return "People";
            if (_places[_currentPlayer] == 11 || _places[_currentPlayer] == 8) return "Rap";
            if (_places[_currentPlayer] == 12 || _places[_currentPlayer] == 9) return "Philosophy";
            if (_places[_currentPlayer] == 13|| _places[_currentPlayer] == 10) return "Literature";
            if (_places[_currentPlayer] == 14) return "Geography";
            if (_technoQuestions.Any()) return "Techno";
            
            return "Rock";
        }


        // if the player answered correctly
        public bool WasCorrectlyAnswered()
        {
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
                        return false;
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
                    return false;
                }

                _currentPlayer++;
                if (_currentPlayer == _players.Count) _currentPlayer = 0;
            }

            return true;
        }



        // if the player answered wrong
        public bool WrongAnswer()
        {
            console.WriteLine("Question was incorrectly answered");
            console.WriteLine($"{_players[_currentPlayer]} was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;
            _timesInPrison[_currentPlayer]++;
            _turnInPrison[_currentPlayer] = 0;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            return true;
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