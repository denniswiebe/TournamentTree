using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TournamentTree.UnitTests
{
    [TestClass]
    public class TournamentGeneratorTest
    {
        [TestMethod]
        public void AmountOfPlayersIsPowerOfTwo()
        {
            FakeObjects.FakeTournamentGenerator generator = new FakeObjects.FakeTournamentGenerator();

            // Test, ob Spieleranzahl eine Potenz von 2 ist
            // Müsste TRUE sein, da 4 = 2²
            Assert.IsTrue(generator.CheckIfAmountOfPlayersIsPowerOfTwo(generator.FourPlayers.Count));
        }

        [TestMethod]
        public void AmountOfPlayersIsNotPowerOfTwo()
        {
            FakeObjects.FakeTournamentGenerator generator = new FakeObjects.FakeTournamentGenerator();

            // Test, ob Spieleranzahl eine Potenz von 2 ist
            // FALSE, da 3 keine Potenz von 2 ist
            Assert.IsFalse(generator.CheckIfAmountOfPlayersIsPowerOfTwo(generator.ThreePlayers.Count));
        }
    }
}
