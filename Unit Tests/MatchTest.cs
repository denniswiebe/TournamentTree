using Microsoft.VisualStudio.TestTools.UnitTesting;
using TournamentTree;

namespace TestProject2
{
    [TestClass]
    public class MatchTest
    {

        [TestMethod]
        public void ChangeHomeAndAway()
        {
            Player playerOne = new Player("PlayerOne");
            Player playerTwo = new Player("PlayerTwo");
            Match match = new Match(playerOne, playerTwo);

            match.ChangeHomeAndAway();

            Assert.AreEqual(match.PlayerOne, playerTwo);
            Assert.AreEqual(match.PlayerTwo, playerOne);
        }

        [TestMethod]
        public void CheckInputOfMatchTest()
        {
            Player playerOne = new Player("PlayerOne");
            Player playerTwo = new Player("PlayerTwo");
            Match match = new Match(playerOne, playerTwo);

            bool checkedInput = match.CheckInputOfMatch("4 2");

            Assert.AreEqual(true, checkedInput);
        }
    }
}
