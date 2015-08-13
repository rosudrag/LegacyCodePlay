using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class QuestionGame
    {


        List<string> players = new List<string>();

        int[] places = new int[6];
        int[] purses = new int[6];

        bool[] inPenaltyBox = new bool[6];

        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        int _currentPlayerId = 0;
        bool _evenNumberRolled;

        public QuestionGame()
        {
            for (int i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        public String CreateRockQuestion(int questionIndex)
        {
            return "Rock Question " + questionIndex;
        }

        public bool IsPlayable()
        {
            return (PlayerCount() >= 2);
        }

        public bool AddPlayer(String playerName)
        {


            players.Add(playerName);
            places[PlayerCount()] = 0;
            purses[PlayerCount()] = 0;
            inPenaltyBox[PlayerCount()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int PlayerCount()
        {
            return players.Count;
        }

        public void TakeTurn(int rollNumber)
        {
            Console.WriteLine(players[_currentPlayerId] + " is the current player");
            Console.WriteLine("They have rolled a " + rollNumber);

            if (IsPlayerInPenaltyBox())
            {
                if (IsNumberEven(rollNumber))
                {
                    _evenNumberRolled = true;

                    Console.WriteLine(players[_currentPlayerId] + " is getting out of the penalty box");
                    places[_currentPlayerId] = places[_currentPlayerId] + rollNumber;
                    if (places[_currentPlayerId] > 11) places[_currentPlayerId] = places[_currentPlayerId] - 12;

                    Console.WriteLine(players[_currentPlayerId]
                            + "'s new location is "
                            + places[_currentPlayerId]);
                    Console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(players[_currentPlayerId] + " is not getting out of the penalty box");
                    _evenNumberRolled = false;
                }

            }
            else
            {

                places[_currentPlayerId] = places[_currentPlayerId] + rollNumber;
                if (IsCurrentPlayerPast11()) places[_currentPlayerId] = places[_currentPlayerId] - 12;

                Console.WriteLine(players[_currentPlayerId]
                        + "'s new location is "
                        + places[_currentPlayerId]);
                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }

        }

        private bool IsCurrentPlayerPast11()
        {
            return places[_currentPlayerId] > 11;
        }

        private bool IsPlayerInPenaltyBox()
        {
            return inPenaltyBox[_currentPlayerId];
        }

        private static bool IsNumberEven(int rollNumber)
        {
            return rollNumber % 2 != 0;
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                Console.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Rock")
            {
                Console.WriteLine(rockQuestions.First());
                rockQuestions.RemoveFirst();
            }
        }


        private String CurrentCategory()
        {
            if (places[_currentPlayerId] == 0) return "Pop";
            if (places[_currentPlayerId] == 4) return "Pop";
            if (places[_currentPlayerId] == 8) return "Pop";
            if (places[_currentPlayerId] == 1) return "Science";
            if (places[_currentPlayerId] == 5) return "Science";
            if (places[_currentPlayerId] == 9) return "Science";
            if (places[_currentPlayerId] == 2) return "Sports";
            if (places[_currentPlayerId] == 6) return "Sports";
            if (places[_currentPlayerId] == 10) return "Sports";
            return "Rock";
        }

        public bool AnnounceCorrectAnswer()
        {
            if (IsPlayerInPenaltyBox())
            {
                if (_evenNumberRolled)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    purses[_currentPlayerId]++;
                    Console.WriteLine(players[_currentPlayerId]
                            + " now has "
                            + purses[_currentPlayerId]
                            + " Gold Coins.");

                    bool result = DidPlayerLose();
                    _currentPlayerId++;
                    if (IsLastPlayer()) _currentPlayerId = 0;

                    return result;
                }
                else
                {
                    _currentPlayerId++;
                    if (IsLastPlayer()) _currentPlayerId = 0;
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                purses[_currentPlayerId]++;
                Console.WriteLine(players[_currentPlayerId]
                        + " now has "
                        + purses[_currentPlayerId]
                        + " Gold Coins.");

                bool result = DidPlayerLose();
                _currentPlayerId++;
                if (IsLastPlayer()) _currentPlayerId = 0;

                return result;
            }
        }

        private bool IsLastPlayer()
        {
            return _currentPlayerId == players.Count;
        }

        public bool AnnounceWrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[_currentPlayerId] + " was sent to the penalty box");
            inPenaltyBox[_currentPlayerId] = true;

            _currentPlayerId++;
            if (IsLastPlayer()) _currentPlayerId = 0;
            return true;
        }


        private bool DidPlayerLose()
        {
            return !(purses[_currentPlayerId] == 6);
        }
    }

}
