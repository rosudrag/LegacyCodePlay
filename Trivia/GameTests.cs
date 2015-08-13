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
            var myGame = new Game();

            myGame.add("myPlayer");

            Assert.AreEqual(myGame.howManyPlayers(), 1);
        }

        [Test]
        public void GameIsNotPlayableWithoutPlayers()
        {
            var myGame = new Game();

            var playable = myGame.isPlayable();

            Assert.False(playable);
        }

        [Test]
        public void GameIsNotPlayableWithOnePlayer()
        {
            var myGame = new Game();

            myGame.add("player one");

            Assert.False(myGame.isPlayable());
        }

        [Test]
        public void GameIsPlayableWithTwoPlayers()
        {
            var myGame = new Game();

            myGame.add("player one");
            myGame.add("player two");

            Assert.True(myGame.isPlayable());
        }

        [Test]
        public void GameIsNotPlayableWithMoreThan5Players()
        {
            var myGame = new Game();

            for (int i = 0; i <= 4; i++)
            {
                myGame.add("player " + i);
            }

            Assert.Throws<IndexOutOfRangeException>(() => { myGame.add("I crash"); });

        }

        [Test]
        public void NewGameHasZeroPlayers()
        {
            var myGame = new Game();

            Assert.AreEqual(myGame.howManyPlayers(), 0);
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

            Assert.AreEqual(expectedOutput, output, "Game output does not match the golden master");
        }

    }
}