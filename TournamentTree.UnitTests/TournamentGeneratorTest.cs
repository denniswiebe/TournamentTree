using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TournamentTree.UnitTests
{
    [TestClass]
    public class TournamentGeneratorTest
    {
        [TestMethod]
        public void AmountOfPlayersIsPowerOfTwo()
        {
            // Erzeugen eines Moc
            TournamentGenerator generator = new TournamentGenerator();
            var playerOne = new Player("PlayerOne", 1);
            var playerTwo = new Player("PlayerTwo", 2);
            var playerThree = new Player("PlayerThree", 3);
            var playerFour = new Player("PlayerFour", 4);

            generator.AllPlayers.Add(playerOne);
            generator.AllPlayers.Add(playerTwo);
            generator.AllPlayers.Add(playerThree);
            generator.AllPlayers.Add(playerFour);

            // Test, ob Spieleranzahl eine Potenz von 2 ist
            // Müsste TRUE sein, da 4 = 2²
            Assert.IsTrue(generator.CheckIfAmountOfPlayersIsPowerOfTwo());
        }

        [TestMethod]
        public void AmountOfPlayersIsNotPowerOfTwo()
        {
            TournamentGenerator generator = new TournamentGenerator();
            var playerOne = new Player("PlayerOne", 1);
            var playerTwo = new Player("PlayerTwo", 2);
            var playerThree = new Player("PlayerThree", 3);

            generator.AllPlayers.Add(playerOne);
            generator.AllPlayers.Add(playerTwo);
            generator.AllPlayers.Add(playerThree);
            
            // Test, ob Spieleranzahl eine Potenz von 2 ist
            // FALSE, da 3 keine Potenz von 2 ist
            Assert.IsFalse(generator.CheckIfAmountOfPlayersIsPowerOfTwo());
        }
    }
}
