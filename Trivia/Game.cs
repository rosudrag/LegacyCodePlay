using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class Game
    {


        List<string> players = new List<string>();

        int[] places = new int[6];
        int[] purses = new int[6];

        bool[] inPenaltyBox = new bool[6];

        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        private readonly Dictionary<String, LinkedList<String>> CategoryToQuestions;

        readonly Dictionary<int, string> categoryTypes = new Dictionary<int, string>()
            {
                {0,"Pop"},
                {4,"Pop"},
                {8,"Pop"},
                {1,"Science"},
                {5,"Science"},
                {9,"Science"},
                {2,"Sports"},
                {6,"Sports"},
                {10,"Sports"},
            };

        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        public Game()
        {
            for (int i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast(createRockQuestion(i));
            }

            CategoryToQuestions = new Dictionary<String, LinkedList<String>>()
            {
                {"Pop", popQuestions},
                {"Rock", rockQuestions},
                {"Science", scienceQuestions},
                {"Sports", sportsQuestions},
            };   
        }

        public String createRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(String playerName)
        {


            players.Add(playerName);
            places[howManyPlayers()] = 0;
            purses[howManyPlayers()] = 0;
            inPenaltyBox[howManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return players.Count;
        }

        public void roll(int roll)
        {
            Console.WriteLine(players[currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (inPenaltyBox[currentPlayer])
            {
                isGettingOutOfPenaltyBox = roll%2 != 0;
                if (roll%2 == 0)
                {
                    Console.WriteLine(players[currentPlayer] + " is not getting out of the penalty box");
                    
                    return;
                }
                Console.WriteLine(players[currentPlayer] + " is getting out of the penalty box");
            }
            
            MovePlayerAndAskQuestion(roll);
        }

        private void MovePlayerAndAskQuestion(int roll)
        {
            places[currentPlayer] = places[currentPlayer] + roll;
            places[currentPlayer] %= 12;

            Console.WriteLine(players[currentPlayer]
                              + "'s new location is "
                              + places[currentPlayer]);
            Console.WriteLine("The category is " + currentCategory());
            askQuestion();
        }

        private void askQuestion()
        {
            var questions = CategoryToQuestions[currentCategory()];

            Console.WriteLine(questions.First());
            questions.RemoveFirst();
        }


        private String currentCategory()
        {
            try
            {
                return categoryTypes[places[currentPlayer]];
            }
            catch (KeyNotFoundException)
            {
                return "Rock";
            }

        }

        public bool wasCorrectlyAnswered()
        {
            bool winner;

            if (inPenaltyBox[currentPlayer])
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    purses[currentPlayer]++;
                    Console.WriteLine(players[currentPlayer]
                            + " now has "
                            + purses[currentPlayer]
                            + " Gold Coins.");

                    winner = didPlayerWin();
                    currentPlayer++;
                    currentPlayer %= players.Count;

                    return winner;
                }
                currentPlayer++;
                currentPlayer %= players.Count;
                return true;
            }

            Console.WriteLine("Answer was corrent!!!!");
            purses[currentPlayer]++;
            Console.WriteLine(players[currentPlayer]
                              + " now has "
                              + purses[currentPlayer]
                              + " Gold Coins.");

            winner = didPlayerWin();
            currentPlayer++;
            currentPlayer %= players.Count;

            return winner;
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;

            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }


        private bool didPlayerWin()
        {
            return !(purses[currentPlayer] == 6);
        }
    }

}
