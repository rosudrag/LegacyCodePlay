namespace Trivia
{
    using System;
    using System.IO;
    using System.Text;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;
    using UglyTrivia;

    [TestFixture]
    public class GameTests
    {
        [Test]
        public void MyGameHasOnePlayerAfterAddingOnePlayer()
        {
            var myGame = new QuestionGame();

            myGame.AddPlayer("myPlayer");

            Assert.AreEqual(myGame.PlayerCount(), 1);
        }

        [Test]
        public void GameIsNotPlayableWithoutPlayers()
        {
            var myGame = new QuestionGame();

            var playable = myGame.IsPlayable();

            Assert.False(playable);
        }

        [Test]
        public void GameIsNotPlayableWithOnePlayer()
        {
            var myGame = new QuestionGame();

            myGame.AddPlayer("player one");

            Assert.False(myGame.IsPlayable());
        }

        [Test]
        public void GameIsPlayableWithTwoPlayers()
        {
            var myGame = new QuestionGame();

            myGame.AddPlayer("player one");
            myGame.AddPlayer("player two");

            Assert.True(myGame.IsPlayable());
        }

        [Test]
        public void GameIsNotPlayableWithMoreThan5Players()
        {
            var myGame = new QuestionGame();

            for (int i = 0; i <= 4; i++)
            {
                myGame.AddPlayer("player " + i);
            }

            Assert.Throws<IndexOutOfRangeException>(() => { myGame.AddPlayer("I crash"); });

        }

        [Test]
        public void NewGameHasZeroPlayers()
        {
            var myGame = new QuestionGame();

            Assert.AreEqual(myGame.PlayerCount(), 0);
        }

        [Test]
        [Ignore]
        public void GenerateGoldenMasterOutput()
        {
            var builder = new StringBuilder();
            var logger = new StringWriter(builder);
            Console.SetOut(logger);

            for (int i = 0; i < 1000; i++)
            {
                GameRunner.RunGame(new Random(i));
            }

            var output = builder.ToString();

            File.WriteAllText(Path.GetTempPath() + "golden_master.txt", output);
        }

        [Test]
        public void TestGameOutputAgainstGoldenMaster()
        {
            var expectedOutput = File.ReadAllText(Path.GetTempPath() + "golden_master.txt");

            var builder = new StringBuilder();
            var logger = new StringWriter(builder);
            Console.SetOut(logger);

            for (int i = 0; i < 1000; i++)
            {
                GameRunner.RunGame(new Random(i));
            }

            var output = builder.ToString();

            Assert.AreEqual(expectedOutput, output, "QuestionGame output does not match the golden master");
        }

    }
}