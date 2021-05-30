using Microsoft.VisualStudio.TestTools.UnitTesting;
using TournamentTree.Core;

namespace TournamentTree.UnitTests
{
    [TestClass]
    public class MatchTest
    {
        [TestMethod]
        public void ChangeHomeAndAway()
        {
            Player playerOne = new Player(new Name("PlayerOne"), new Identification(1));
            Player playerTwo = new Player(new Name("PlayerTwo"), new Identification(2));
            Match match = new Match(playerOne, playerTwo);

            var matchFactory = new MatchFactory();
            matchFactory.ChangeHomeAndAway(match);

            Assert.AreEqual(match.PlayerOne, playerTwo);
            Assert.AreEqual(match.PlayerTwo, playerOne);
        }

        [TestMethod]
        public void CheckInputOfMatchTest()
        {
            Player playerOne = new Player(new Name("PlayerOne"), new Identification(1));
            Player playerTwo = new Player(new Name("PlayerTwo"), new Identification(2));
            Match match = new Match(playerOne, playerTwo);

            var matchFactory = new MatchFactory();
            bool checkedInputTrue = matchFactory.CheckInputOfMatch("4 2");
            bool checkedInputFalse = matchFactory.CheckInputOfMatch("a 1");
            bool checkedInputFalse2 = matchFactory.CheckInputOfMatch("12");
            bool checkedInputFalse3 = matchFactory.CheckInputOfMatch("1 a");

            Assert.AreEqual(true, checkedInputTrue);
            Assert.AreEqual(false, checkedInputFalse);
            Assert.AreEqual(false, checkedInputFalse2);
            Assert.AreEqual(false, checkedInputFalse3);
        }
    }
}
