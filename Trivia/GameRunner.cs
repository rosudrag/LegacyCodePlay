using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UglyTrivia;

namespace Trivia
{
    public class GameRunner
    {

        private static bool _canContinueGame;

        public static void Main(String[] args)
        {
            RunGame(new Random());
        }

        public static void RunGame(Random random)
        {
            QuestionGame aQuestionGame = new QuestionGame();

            aQuestionGame.AddPlayer("Chet");
            aQuestionGame.AddPlayer("Pat");
            aQuestionGame.AddPlayer("Sue");

            do
            {

                aQuestionGame.TakeTurn(random.Next(5) + 1);

                if (random.Next(9) == 7)
                {
                    _canContinueGame = aQuestionGame.AnnounceWrongAnswer();
                }
                else
                {
                    _canContinueGame = aQuestionGame.AnnounceCorrectAnswer();
                }
            } while (_canContinueGame);
        }


    }

}

