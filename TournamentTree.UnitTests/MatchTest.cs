using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TournamentTree.UnitTests
{
    [TestClass]
    public class MatchTest
    {
        [TestMethod]
        public void ChangeHomeAndAway()
        {
            Player playerOne = new Player("PlayerOne", 1);
            Player playerTwo = new Player("PlayerTwo", 2);
            Match match = new Match(playerOne, playerTwo);

            match.ChangeHomeAndAway();

            Assert.AreEqual(match.PlayerOne, playerTwo);
            Assert.AreEqual(match.PlayerTwo, playerOne);
        }

        [TestMethod]
        public void CheckInputOfMatchTest()
        {
            Player playerOne = new Player("PlayerOne", 1);
            Player playerTwo = new Player("PlayerTwo", 2);
            Match match = new Match(playerOne, playerTwo);

            bool checkedInputTrue = match.CheckInputOfMatch("4 2");
            bool checkedInputFalse = match.CheckInputOfMatch("a 1");
            bool checkedInputFalse2 = match.CheckInputOfMatch("12");
            bool checkedInputFalse3 = match.CheckInputOfMatch("1 a");

            Assert.AreEqual(true, checkedInputTrue);
            Assert.AreEqual(false, checkedInputFalse);
            Assert.AreEqual(false, checkedInputFalse2);
            Assert.AreEqual(false, checkedInputFalse3);
        }
    }
}
